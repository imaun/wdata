using System.Text;
using Microsoft.Extensions.Options;
using Wdata.Contracts;
using Wdata.Configuration;

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

    public WebsiteLocalSource(IOptions<WdataConfig> config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var local_config = config.Value.GetLocalSource();
        if (local_config is not null)
        {
            _basePath = local_config.BasePath;
        }
    }
    
    public async Task<string> FetchDataAsync(string path, CancellationToken cancel = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
        
        if (File.Exists(path))
            return await File.ReadAllTextAsync(path, Encoding.UTF8, cancel);

        if (string.IsNullOrWhiteSpace(_basePath))
            throw new ArgumentNullException("Base path is empty");
        
        var fullPath = Path.Combine(_basePath, path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"File not found: {fullPath}");
        
        return await File.ReadAllTextAsync(fullPath, Encoding.UTF8, cancel).ConfigureAwait(false);
    }
}