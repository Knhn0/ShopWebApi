using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Application.Interfaces;

public interface IProductService
{
    public Task<Guid> Add(Product product);
}