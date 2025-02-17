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
        await using var connection = new NpgsqlConnection(_dbConnection);

        var sql =
            @"insert into ""Products""(""Definition"", ""Name"", ""Price"", ""Image"") 
            values (@Definition, @Name, @Price, @Image) 
            returning ""Id""
            ";
            
        var id = await connection.QueryFirstOrDefaultAsync<Guid>(sql, product);
        await connection.CloseAsync();
            
        return id;
    }
}