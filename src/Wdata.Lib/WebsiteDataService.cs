using Microsoft.Extensions.Options;
using Wdata.Contracts;
using Wdata.Lib.Configuration;
using Wdata.Models;

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
    
    public async Task<WebsiteData> GetWebsiteDataAsync(string path, CancellationToken cancel = default)
    {
        throw new NotImplementedException();
    }
}