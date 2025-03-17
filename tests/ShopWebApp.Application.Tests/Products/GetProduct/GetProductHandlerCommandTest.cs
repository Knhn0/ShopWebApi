namespace ShopWebApp.Application.Tests.Products.GetProduct
{
    public class GetProductHandlerCommandTest
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
        public async Task GetProductByIdAsync_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика успешного получения продукта по ID
            productServiceMock
                .Setup(service => service.GetByIdAsync(_validProduct.Id))
                .ReturnsAsync(_validProduct);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.GetProductByIdAsync(_validProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(_validProduct.Id, returnedProduct.Id);
            Assert.Equal(_validProduct.Name, returnedProduct.Name);
            Assert.Equal(_validProduct.Description, returnedProduct.Description);
            Assert.Equal(_validProduct.Price, returnedProduct.Price);
            Assert.Equal(_validProduct.Image, returnedProduct.Image);

            // Проверяем, что метод GetByIdAsync вызывается один раз с правильным ID
            productServiceMock.Verify(service => service.GetByIdAsync(_validProduct.Id), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика возврата null при отсутствии продукта по ID
            productServiceMock
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Product)null);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.GetProductByIdAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);

            // Убеждаемся, что метод GetByIdAsync вызывается один раз
            productServiceMock.Verify(service => service.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }

    // Mock интерфейса IProductService
    public interface IProductService
    {
        Task<Product> GetByIdAsync(Guid id);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
