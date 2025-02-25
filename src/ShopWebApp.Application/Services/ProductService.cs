using Microsoft.AspNetCore.JsonPatch;
using ShopWebApp.Application.Interfaces;
using ShopWebApp.Domain.Entities;
using ShopWebApp.Domain.Interfaces;

namespace ShopWebApp.Application.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Guid> AddAsync(Product product)
    {
        return await productRepository.AddAsync(product);
    }

    public async Task<Product> DeleteAsync(Guid id)
    {
        return await productRepository.DeleteAsync(id);
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await productRepository.GetByIdAsync(id);
    }

    public async Task<Product> UpdatePartialAsync(Guid id, Product productForUpdate)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("Product with such id does not exist");
        }
        
        var updatedProduct = await productRepository.UpdateAsync(new Product
        {
            Id = id,
            Name = productForUpdate.Name,
            Price = productForUpdate.Price,
            Definition = productForUpdate.Definition,
            Image = productForUpdate.Image
        });

        return updatedProduct;
    }
}