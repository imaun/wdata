using Wdata.Models;

namespace Wdata.Contracts;

public interface IWebsiteDataService
{

    Task<WebsiteData?> GetWebsiteDataAsync(string source, string path, CancellationToken cancel = default);
    
    Task<WebsiteData?> GetWebsiteDataAsync(string path, CancellationToken cancel = default);

    Task<WebsitePost?> GetWebsitePostAsync(string source, string path, CancellationToken cancel = default);
    
    Task<WebsitePost?> GetWebsitePostAsync(string path, CancellationToken cancel = default);

    Task<string> GetWebsiteContent(string source, string path, string mode = "html", CancellationToken cancel = default);
    
    Task<string> GetWebsiteContent(string path, string mode = "html", CancellationToken cancel = default);
}