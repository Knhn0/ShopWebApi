namespace ShopWebApp.Application.Tests.Products.UpdateProduct
{
    public class UpdateProductHandlerCommandTest
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
        public async Task UpdateProductAsync_ShouldReturnOk_WhenProductIsUpdatedSuccessfully()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика успешного обновления продукта
            productServiceMock
                .Setup(service => service.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.UpdateProductAsync(_validProduct);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);

            // Проверяем через мок, что UpdateAsync действительно вызывался с правильной моделью
            productServiceMock.Verify(service => service.UpdateAsync(It.Is<Product>(
                p => p.Id == _validProduct.Id &&
                     p.Name == _validProduct.Name &&
                     p.Description == _validProduct.Description &&
                     p.Price == _validProduct.Price &&
                     p.Image == _validProduct.Image
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика возврата false при отсутствии продукта
            productServiceMock
                .Setup(service => service.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(false);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.UpdateProductAsync(_validProduct);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            // Убеждаемся, что UpdateAsync действительно вызывался только один раз
            productServiceMock.Verify(service => service.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnBadRequest_WhenProductIsNull()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.UpdateProductAsync(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);

            // Убеждаемся, что UpdateAsync не вызывается, если передан null
            productServiceMock.Verify(service => service.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }

    // Mock интерфейса IProductService
    public interface IProductService
    {
        Task<bool> UpdateAsync(Product product);
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

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var isUpdated = await _productService.UpdateAsync(product);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
