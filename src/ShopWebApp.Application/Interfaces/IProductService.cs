using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Application.Interfaces;

public interface IProductService
{
    public Task<int> Add(Product product);
}