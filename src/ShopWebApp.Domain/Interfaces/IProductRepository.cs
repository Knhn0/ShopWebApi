using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Domain.Interfaces;

public interface IProductRepository
{
    public Task<Guid> AddAsync(Product product);
    public Task<Product> DeleteAsync(Guid id);
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> UpdateAsync(Product product);
}