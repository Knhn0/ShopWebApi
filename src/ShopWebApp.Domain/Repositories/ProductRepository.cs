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

    
    public async Task<int> AddAsync(Product product)
    {
        await using var connection = new NpgsqlConnection(_dbConnection);
        
        var sql = @"INSERT INTO ""Products"" (""Name"")
                    VALUES (@Name)
                    RETURNING ""Id"";";

        var id = await connection.ExecuteScalarAsync<int>(sql, product);
        await connection.CloseAsync();

        return id;
    }
}