using Microsoft.Extensions.DependencyInjection;
using ShopWebApp.Domain.Interfaces;
using ShopWebApp.Domain.Repositories;

namespace EmployeeWebApp.Domain.Extensions;

/// <summary>
/// Класс с методами расширения для добавления зависимостей слоя Domain.
/// </summary>
public static class DomainExtensions
{
    /// <summary>
    /// Добавляет в контейнер зависимостей реализации для интерфейсов репозиториев.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProductRepository, ProductRepository>();
    }
}