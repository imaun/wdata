using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wdata.Contracts;
using Wdata.Configuration;
using Wdata.Extensions;
using Wdata.Models;
using Wdata.Sources;
using Wdata.Parsers;

namespace Wdata;

public class WebsiteDataService : IWebsiteDataService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WebsiteDataService> _logger;
    private readonly WdataConfig _config;
    
    public WebsiteDataService(
        IServiceProvider serviceProvider,
        IOptions<WdataConfig> options,
        ILogger<WebsiteDataService> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _config = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<WebsiteData?> GetWebsiteDataAsync(string source, string path, CancellationToken cancel = default)
    {
        if(string.IsNullOrWhiteSpace(source))
            throw new ArgumentNullException(nameof(source));
        
        var dataSource = getDataSource(source);

        try
        {
            var content = await dataSource.FetchDataAsync(path, cancel);

            var jsonParser = new JsonParser<WebsiteData>();
            return jsonParser.Parse(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, getLogExceptionText(ex));
            return null;
        }
    }
    
    public async Task<WebsiteData?> GetWebsiteDataAsync(string path, CancellationToken cancel = default)
    {
        return await GetWebsiteDataAsync(_config.DefaultSource, path, cancel);
    }

    public async Task<WebsitePost?> GetWebsitePostAsync(string source, string path, CancellationToken cancel = default)
    {
        if(string.IsNullOrWhiteSpace(source))
            throw new ArgumentNullException(nameof(source));
        
        var dataSource = getDataSource(source);

        string content = "";

        try
        {
            content = await dataSource.FetchDataAsync(path, cancel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, getLogExceptionText(ex));
            return null;
        }

        var parser = new MarkdownParser();
        var result = parser.Parse(content);

        var post = result.ToWebsitePost();

        if (!string.IsNullOrWhiteSpace(post.Ref))
        {
            try
            {
                var refContent = await dataSource.FetchDataAsync(post.Ref, cancel);
                if (!string.IsNullOrWhiteSpace(refContent))
                {
                    if(post.RefMode is "md" or "markdown")
                    {
                        var ref_html = parser.ConvertToHtml(refContent);
                        post.AddToBody(ref_html);
                    }
                    else
                    {
                        post.AddToBody(refContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, getLogRefNotFoundExceptionText(ex));
            }
        }

        return post;
    }

    public async Task<WebsitePost?> GetWebsitePostAsync(string path, CancellationToken cancel = default)
    {
        return await GetWebsitePostAsync(_config.DefaultSource, path, cancel);
    }
    
    public async Task<string> GetWebsiteContent(
        string source, string path, string mode = "html", CancellationToken cancel = default)
    {
        if(string.IsNullOrWhiteSpace(source))
            throw new ArgumentNullException(nameof(source));
        
        var dataSource = getDataSource(source);

        string content = "";

        try
        {
            content = await dataSource.FetchDataAsync(path, cancel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, getLogExceptionText(ex));
        }
        
        if (mode is "md" or "markdown")
        {
            var parser = new MarkdownParser();
            var body = parser.Parse(content).body;
            return body;
        }

        return content;
    }

    public async Task<string> GetWebsiteContent(
        string path, string mode = "html", CancellationToken cancel = default)
    {
        return await GetWebsiteContent(_config.DefaultSource, path, mode, cancel);
    }


    public async Task<WebsitePostIndex?> GetPostIndexAsync(
        string source, string path, CancellationToken cancel = default)
    {
        if(string.IsNullOrWhiteSpace(source))
            throw new ArgumentNullException(nameof(source));
        
        var dataSource = getDataSource(source);
        
        string content = "";

        try
        {
            content = await dataSource.FetchDataAsync(path, cancel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, getLogExceptionText(ex));
        }
        
        var jsonParser = new JsonParser<WebsitePostIndex>();
        return jsonParser.Parse(content);
    }

    public async Task<WebsitePostIndex?> GetPostIndexAsync(string path, CancellationToken cancel = default)
    {
        return await GetPostIndexAsync(_config.DefaultSource, path, cancel);
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

    private static string getLogExceptionText(Exception ex)
    {
        return $"[Wdata-Exception]: {ex.GetBaseException().Message}";
    }

    private static string getLogRefNotFoundExceptionText(Exception ex)
    {
        return $"[Wdata-Exception]:[RefNotFound]: {ex.GetBaseException().Message}";
    }
}