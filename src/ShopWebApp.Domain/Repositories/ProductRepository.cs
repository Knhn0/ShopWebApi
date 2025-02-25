using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ShopWebApp.Domain.Entities;
using ShopWebApp.Domain.Interfaces;

namespace ShopWebApp.Domain.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _dbConnection;

    public ProductRepository(IConfiguration configuration)
    {
        _dbConnection = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<Guid> AddAsync(Product product)
    {
        await using (var connection = new NpgsqlConnection(_dbConnection))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var sql = @"insert into ""Products""(""Description"", ""Name"", ""Price"", ""Image"") 
                values (@Description, @Name, @Price, @Image) 
                returning ""Id""";

                var id = await connection.QueryFirstOrDefaultAsync<Guid>(sql, product);
                transaction.Commit();

                return id;
            }
        }
    }

    public async Task<Product> DeleteAsync(Guid id)
    {
        await using (var connection = new NpgsqlConnection(_dbConnection))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var query = @"with deleted as (
                          delete from ""Products"" 
                          where ""Id"" = @id 
                          returning *)
                          select * from deleted;";

                var product = await connection.QueryFirstOrDefaultAsync<Product>(query, new {Id = id});
                transaction.Commit();

                return product;
            }
        }
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        await using (var connection = new NpgsqlConnection(_dbConnection))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var query = @"select * from ""Products"" where ""Id"" = @Id";
                var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new {Id = id});
                transaction.Commit();

                return product;
            }
        }
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        await using (var connection = new NpgsqlConnection(_dbConnection))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var query = @"
                update ""Products""
                set 
                    ""Name"" = @Name,
                    ""Description"" = @Description,
                    ""Price"" = @Price
                where ""Id"" = @Id
                returning *;";

                var updatedProduct = await connection.QueryFirstOrDefaultAsync<Product>(query, new
                {
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Id
                });
                transaction.Commit();

                return updatedProduct;
            }
        }
    }

    public async Task<Product> GetByNameAsync(string productName)
    {
        await using (var connection = new NpgsqlConnection(_dbConnection))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var query = @"select * from ""Products"" where ""Name"" = @Name";
                var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new {Name = productName});
                transaction.Commit();

                return product;
            }
        }
    }
}