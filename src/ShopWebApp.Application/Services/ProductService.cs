using ShopWebApp.Application.Interfaces;
using ShopWebApp.Domain.Entities;
using ShopWebApp.Domain.Interfaces;

namespace ShopWebApp.Application.Products;

public class ProductService (IProductRepository productRepository) : IProductService
{
    public async Task<Guid> Add(Product product)
    {
        return await productRepository.AddAsync(product);
    }
}