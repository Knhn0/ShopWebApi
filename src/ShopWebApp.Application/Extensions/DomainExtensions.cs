using Microsoft.Extensions.DependencyInjection;
using ShopWebApp.Application.Interfaces;
using ShopWebApp.Application.Products;

namespace EmployeeWebApp.Domain.Extensions;

/// <summary>
/// Класс с методами расширения для добавления зависимостей слоя Domain.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Добавляет в контейнер зависимостей реализации для интерфейсов репозиториев.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IProductService, ProductService>();
    }
}