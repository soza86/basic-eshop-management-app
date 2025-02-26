using CSharpApp.Infrastructure.ExternalServices;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.Configure<RestApiSettings>(configuration!.GetSection(nameof(RestApiSettings)));
        services.Configure<HttpClientSettings>(configuration.GetSection(nameof(HttpClientSettings)));

        services.AddHttpClient<IJwtTokenService, JwtTokenService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        });

        services.AddHttpClient<IProductsService, ProductsService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        }).AddHttpMessageHandler<JwtAuthHandler>();

        services.AddHttpClient<ICategoriesService, CategoriesService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        }).AddHttpMessageHandler<JwtAuthHandler>();

        services.AddTransient<JwtAuthHandler>();

        return services;
    }
}