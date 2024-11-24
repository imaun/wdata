using Wdata.Contracts;

namespace Wdata.Sources;

public class WebsiteRemoteSource : IWebsiteDataSource
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public WebsiteRemoteSource(string baseUrl, HttpClient httpClient)
    {
        _baseUrl = baseUrl;
        _httpClient = httpClient;
    }
    
    public Task<string> FetchDataAsync(string path, CancellationToken cancel = default)
    {
        throw new NotImplementedException();
    }
}