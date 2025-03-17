namespace ShopWebApp.Application.Tests.Products.DeleteProduct
{
    public class DeleteProductHandlerCommandTest
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
        public async Task DeleteProductAsync_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика успешного удаления продукта
            productServiceMock
                .Setup(service => service.DeleteAsync(_validProduct.Id))
                .ReturnsAsync(true);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.DeleteProductAsync(_validProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);

            // Проверяем через мок, что DeleteAsync действительно вызывался с правильным Id
            productServiceMock.Verify(service => service.DeleteAsync(_validProduct.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            // Стаб: Логика возврата false при отсутствии продукта
            productServiceMock
                .Setup(service => service.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.DeleteProductAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);

            // Убеждаемся, что DeleteAsync действительно вызывался только один раз
            productServiceMock.Verify(service => service.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }

    // Mock интерфейса IProductService
    public interface IProductService
    {
        Task<bool> DeleteAsync(Guid id);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var isDeleted = await _productService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
