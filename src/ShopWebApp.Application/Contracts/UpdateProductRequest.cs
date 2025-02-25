namespace ShopWebApp.Application.Contracts;

public class UpdateProductRequest
{
    public string Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}