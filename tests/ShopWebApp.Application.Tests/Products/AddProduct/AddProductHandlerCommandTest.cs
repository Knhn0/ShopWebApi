namespace ShopWebApp.Application.Tests.Products.AddProduct;

public class AddProductHandlerCommandTest
{
    // Stub реализации модели Product
    private readonly Product _validProduct = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Test Product",
        Description = "Test Product Description",
        Price = 100.0m,
        Image = "test-image-url"
    };

    [Fact]
    public async Task AddProductAsync_ShouldReturnOk_WhenProductIsValid()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();

        // Стаб: логика "поддельного" сервиса для возврата Id созданного продукта
        var stubProductId = Guid.NewGuid();
        productServiceMock
            .Setup(service => service.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(stubProductId);

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.AddProductAsync(_validProduct);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(stubProductId, okResult.Value);

        // Проверяем через мок, что AddAsync действительно вызывался с правильной моделью
        productServiceMock.Verify(service => service.AddAsync(It.Is<Product>(
            p => p.Name == _validProduct.Name &&
                 p.Description == _validProduct.Description &&
                 p.Price == _validProduct.Price &&
                 p.Image == _validProduct.Image
        )), Times.Once);
    }

    [Fact]
    public async Task AddProductAsync_ShouldReturnBadRequest_WhenProductIsNull()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.AddProductAsync(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);

        // Убеждаемся, что AddAsync не вызывается, если передан null
        productServiceMock.Verify(service => service.AddAsync(It.IsAny<Product>()), Times.Never);
    }
}

// Mock интерфейса IProductService
public interface IProductService
{
    Task<Guid> AddAsync(Product product);
}

// Stub модели Product
public class Product
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}

// Контроллер для тестов
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        var productId = await _productService.AddAsync(product);
        return Ok(productId);
    }
}