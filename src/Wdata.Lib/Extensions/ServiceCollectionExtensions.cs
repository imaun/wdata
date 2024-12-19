using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wdata.Configuration;
using Wdata.Contracts;
using Wdata.Sources;

namespace Wdata.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddWdata(
        this IServiceCollection services, 
        IConfiguration configuration,
        string configSection)
    {
        services.Configure<WdataConfig>(configuration.GetSection(configSection));
        services.AddTransient<IWebsiteDataService, WebsiteDataService>();
        
        return services;
    }
    

    public static IServiceCollection AddWdataLocalSource(this IServiceCollection services)
    {
        services.AddTransient<WebsiteLocalSource>(provider =>
        {
            var config = provider.GetRequiredService<IOptions<WdataConfig>>().Value
                ?? throw new InvalidOperationException("Wdata configuration is missing");
            
            var localSource = config.GetLocalSource() 
                ?? throw new InvalidOperationException("Local source configuration is missing");

            return new WebsiteLocalSource(localSource!.BasePath);
        });

        return services;
    }

    public static IServiceCollection AddWdataRemoteSource(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddTransient<WebsiteRemoteSource>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<WdataConfig>>();
            var config = options.Value ?? throw new InvalidOperationException("Wdata configuration is missing");
            
            var remoteSource = config.GetRemoteSource()
                ?? throw new InvalidOperationException("Remote source configuration is missing");
            
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            
            return new WebsiteRemoteSource(options, httpClient);
        });

        return services;
    }
    
    public static IServiceCollection AddWdata(this IServiceCollection services, IConfiguration configuration)
    {
        return AddWdata(services, configuration, "WebsiteData");
    }
}