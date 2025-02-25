using Microsoft.AspNetCore.JsonPatch;
using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Application.Interfaces;

public interface IProductService
{
    public Task<Guid> AddAsync(Product product);
    public Task<Product> DeleteAsync(Guid id);
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> UpdatePartialAsync(Guid id, Product product);
}