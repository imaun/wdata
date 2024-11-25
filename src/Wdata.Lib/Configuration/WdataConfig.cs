namespace Wdata.Configuration;

/// <summary>
/// Root config schema for Wdata configurations
/// </summary>
public class WdataConfig
{
    public string DefaultSource { get; set; } = null!;
    
    public IReadOnlyCollection<WebsiteSourceConfig> Sources { get; set; }

    public WebsiteSourceConfig? GetLocalSource()
    {
        return Sources.FirstOrDefault(x=> x.Type.Equals("local", StringComparison.OrdinalIgnoreCase));
    }

    public WebsiteSourceConfig? GetRemoteSource()
    {
        return Sources.FirstOrDefault(x=> x.Type.Equals("remote", StringComparison.OrdinalIgnoreCase));
    }

    public WebsiteSourceConfig? GetDefaultSource()
    {
        return Sources.FirstOrDefault(x=> x.Type.Equals(DefaultSource, StringComparison.OrdinalIgnoreCase));
    }
}

public class WebsiteSourceConfig
{
    public string Type { get; set; }
    public string BasePath { get; set; }
}