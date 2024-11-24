using System.Text.Json.Serialization;

namespace Wdata.Models;

public class WebsiteData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("root_url")]
    public string RootUrl { get; set; }

    [JsonPropertyName("domain")]
    public string Domain { get; set; }

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }

    [JsonPropertyName("og_tags")]
    public OgTags OgTags { get; set; }

    [JsonPropertyName("twitter")]
    public Twitter Twitter { get; set; }

    [JsonPropertyName("theme_color")]
    public string ThemeColor { get; set; }

    [JsonPropertyName("favicon")]
    public string Favicon { get; set; }

    [JsonPropertyName("favicon_32")]
    public string Favicon32 { get; set; }

    [JsonPropertyName("favicon_16")]
    public string Favicon16 { get; set; }

    [JsonPropertyName("csp")]
    public string Csp { get; set; }

    [JsonPropertyName("contact")]
    public Contact Contact { get; set; }
}

public class Meta
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("keywords")]
    public string Keywords { get; set; }

    [JsonPropertyName("robots")]
    public string Robots { get; set; }
}

public class OgTags
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class Twitter
{
    [JsonPropertyName("card")]
    public string Card { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("site")]
    public string Site { get; set; }
}

public class Contact
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("wesbite")]
    public string Wesbite { get; set; }
}
