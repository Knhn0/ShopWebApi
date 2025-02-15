
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Application.Interfaces;
using ShopWebApp.Domain.Entities;

namespace ShopWebApp.Controllers;

/// <summary>
/// Product controller
/// </summary>

[Route("api/[controller]")]
[ApiController]

public class ProductController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Add product
    /// </summary>
    /// <returns>Id продукта.</returns>
    [HttpPost]
    public async Task<IActionResult> AddCompanyAsync([FromBody] Product product)
    {
        return Ok(productService.Add(product));
    }
}