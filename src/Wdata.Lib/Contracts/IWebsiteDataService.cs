using Wdata.Models;

namespace Wdata.Contracts;

public interface IWebsiteDataService
{

    Task<WebsiteData?> GetWebsiteDataAsync(string source, string path, CancellationToken cancel = default);
    
    Task<WebsiteData?> GetWebsiteDataAsync(string path, CancellationToken cancel = default);
}