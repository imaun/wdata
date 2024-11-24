using Wdata.Contracts;

namespace Wdata.Sources;

/// <summary>
/// Fetch text data from file system.
/// </summary>
public class WebsiteLocalSource : IWebsiteDataSource
{
    private readonly string _basePath;

    public WebsiteLocalSource(string basePath)
    {
        _basePath = basePath;
    }
    
    public async Task<string> FetchDataAsync(string path, CancellationToken cancel = default)
    {
        var fullPath = Path.Combine(_basePath, path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"File not found: {fullPath}");
        
        return await File.ReadAllTextAsync(fullPath, cancel);
    }
}