using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Domain.Interfaces;

public interface IProductRepository
{
    public Task<Guid> AddAsync(Product product);
    public Task<Product> DeleteAsync(Guid id);
    public Task<Product> GetByIdAsync(Guid id);
    public Task<Product> UpdateAsync(Product product);
    public Task<Product> GetByNameAsync(string productName);
}