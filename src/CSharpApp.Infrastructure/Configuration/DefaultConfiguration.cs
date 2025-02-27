using CSharpApp.Infrastructure.ExternalServices;
using Polly.Extensions.Http;
using Polly;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.Configure<RestApiSettings>(configuration!.GetSection(nameof(RestApiSettings)));
        services.Configure<HttpClientSettings>(configuration.GetSection(nameof(HttpClientSettings)));

        int.TryParse(configuration["HttpClientSettings:RetryCount"], out int retryCount);
        int.TryParse(configuration["HttpClientSettings:SleepDuration"], out int sleepDuration);
        int.TryParse(configuration["HttpClientSettings:LifeTime"], out int lifeTime);

        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                                              .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        var circuitBreakerPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                                              .CircuitBreakerAsync(retryCount, TimeSpan.FromSeconds(sleepDuration));

        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(lifeTime));

        services.AddHttpClient<IJwtTokenService, JwtTokenService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        }).AddPolicyHandler(retryPolicy)
          .AddPolicyHandler(circuitBreakerPolicy)
          .AddPolicyHandler(timeoutPolicy);

        services.AddHttpClient<IProductsService, ProductsService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        }).AddHttpMessageHandler<JwtAuthHandler>()
          .AddPolicyHandler(retryPolicy)
          .AddPolicyHandler(circuitBreakerPolicy)
          .AddPolicyHandler(timeoutPolicy);

        services.AddHttpClient<ICategoriesService, CategoriesService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RestApiSettings:BaseUrl"]!);
        }).AddHttpMessageHandler<JwtAuthHandler>()
          .AddPolicyHandler(retryPolicy)
          .AddPolicyHandler(circuitBreakerPolicy)
          .AddPolicyHandler(timeoutPolicy);

        services.AddTransient<JwtAuthHandler>();

        return services;
    }
}