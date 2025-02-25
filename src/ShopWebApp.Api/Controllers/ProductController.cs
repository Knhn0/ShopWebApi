using System.Data;
using ShopWebApp.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Application.Interfaces;
using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Controllers;

/// <summary>
/// Product controller
/// </summary>
[Route("api/product")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Add product
    /// </summary>
    /// <returns>product Id</returns>
    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] Product product)
    {
        return Ok(await productService.AddAsync(product));
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <returns>product Id</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        return Ok(await productService.DeleteAsync(id));
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <returns>class Product</returns>
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest updateProductRequest)
    {
        var updatedProduct = await productService.UpdatePartialAsync(id,
            new Product
            {
                Definition = updateProductRequest.Definition,
                Price = updateProductRequest.Price,
                Name = updateProductRequest.Name,
                Image = updateProductRequest.Image
            });

        if (updatedProduct == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(updatedProduct);
    }
    
    /// <summary>
    /// Get product
    /// </summary>
    /// <returns>class Product</returns>
    [HttpGet]
    public async Task<IActionResult> GetProduct(string productName)
    {
        return Ok(await productService.GetByNameAsync(productName));
    }
}