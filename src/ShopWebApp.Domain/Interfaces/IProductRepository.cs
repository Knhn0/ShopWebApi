using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Domain.Interfaces;

public interface IProductRepository
{
    public Task<int> AddAsync(Product product);
}