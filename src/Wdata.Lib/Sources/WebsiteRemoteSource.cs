using Microsoft.Extensions.Options;
using Wdata.Contracts;
using Wdata.Configuration;

namespace Wdata.Sources;

/// <summary>
/// Fetch text data from a remote url.
/// </summary>
public class WebsiteRemoteSource : IWebsiteDataSource
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public WebsiteRemoteSource(string baseUrl, HttpClient httpClient)
    {
        _baseUrl = baseUrl;
        _httpClient = httpClient;
    }

    public WebsiteRemoteSource(IOptions<WdataConfig> config, HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(httpClient);

        _httpClient = httpClient;

        var remote_config = config.Value.GetRemoteSource();
        if (remote_config is not null)
        {
            _baseUrl = remote_config.BasePath;
        }
    }
    
    public async Task<string> FetchDataAsync(string path, CancellationToken cancel = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
        
        var url = string.IsNullOrWhiteSpace(_baseUrl)
            ? path.TrimStart('/')
            : $"{_baseUrl.TrimEnd('/')}/{path.TrimStart('/')}";
        
        using var response = await _httpClient.GetAsync(url, cancel);
        return await response.Content.ReadAsStringAsync(cancel);
    }
}