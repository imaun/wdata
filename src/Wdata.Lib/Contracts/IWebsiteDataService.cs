using Wdata.Models;

namespace Wdata.Contracts;

public interface IWebsiteDataService
{
    
    Task<WebsiteData?> GetWebsiteDataAsync(string path, CancellationToken cancel = default);
}