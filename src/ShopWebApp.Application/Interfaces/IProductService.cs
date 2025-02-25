using Microsoft.AspNetCore.JsonPatch;
using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Application.Interfaces;

public interface IProductService
{
    public Task<Guid> AddAsync(Product product);
    public Task<Product> DeleteAsync(Guid id);
    public Task<Product> GetByIdAsync(Guid id);
    public Task<Product> UpdatePartialAsync(Guid id, Product product);
    public Task<Product> GetByNameAsync(string productName);
}