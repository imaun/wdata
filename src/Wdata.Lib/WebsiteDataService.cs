using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wdata.Contracts;
using Wdata.Configuration;
using Wdata.Models;
using Wdata.Sources;
using Wdata.Parsers;

namespace Wdata;

public class WebsiteDataService : IWebsiteDataService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WdataConfig _config;
    
    public WebsiteDataService(
        IServiceProvider serviceProvider,
        IOptions<WdataConfig> options)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _config = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
    
    public async Task<WebsiteData?> GetWebsiteDataAsync(string source, string path, CancellationToken cancel = default)
    {
        if(string.IsNullOrWhiteSpace(source))
            throw new ArgumentNullException(nameof(source));
        
        var dataSource = getDataSource(source);
        
        var content = await dataSource.FetchDataAsync(path, cancel);

        var jsonParser = new JsonParser<WebsiteData>();
        return jsonParser.Parse(content);
    }
    
    public async Task<WebsiteData?> GetWebsiteDataAsync(string path, CancellationToken cancel = default)
    {
        return await GetWebsiteDataAsync(_config.DefaultSource, path, cancel);
    }

    private IWebsiteDataSource getDataSource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            source = _config.DefaultSource;
        
        return source.ToLower() switch
        {
            "local"=> _serviceProvider.GetRequiredService<WebsiteLocalSource>(),
            "remote"=> _serviceProvider.GetRequiredService<WebsiteRemoteSource>(),
            _=> throw new InvalidOperationException($"Unsupported source type: {source}")
        };
    }

    
}