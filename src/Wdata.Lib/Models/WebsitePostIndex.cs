using System.Text.Json.Serialization;

namespace Wdata.Models;

public sealed class WebsitePostIndex
{
    [JsonPropertyName("source")]
    public string Source { get; set; } = null!;

    [JsonPropertyName("post_type")]
    public string PostType { get; set; } = null!;

    [JsonPropertyName("posts")]
    public IReadOnlyCollection<WebsitePostIndexEntry> Posts { get; set; } = new List<WebsitePostIndexEntry>();
}

public sealed class WebsitePostIndexEntry
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = null!;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = null!;

    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
    
    [JsonPropertyName("author")]
    public string? Author { get; set; }
    
    [JsonPropertyName("category")]
    public string? Category { get; set; }
    
    [JsonPropertyName("tags")]
    public string?[]? Tags { get; set; }
    
    [JsonPropertyName("cover_image")]
    public string? CoverImage { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}