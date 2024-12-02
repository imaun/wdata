namespace Wdata.Models;

public sealed class WebsitePostIndex
{
    public string Source { get; set; } = null!;

    public string PostType { get; set; } = null!;

    public IReadOnlyCollection<WebsitePostIndexEntry> Posts { get; set; } = new List<WebsitePostIndexEntry>();
}

public sealed class WebsitePostIndexEntry
{
    public string Path { get; set; } = null!;

    public string Slug { get; set; } = null!;
    
    public string? Title { get; set; }
    
    public string? Summary { get; set; }
    
    public string? Author { get; set; }
    
    public string? Category { get; set; }
    
    public string?[]? Tags { get; set; }
    
    public string? CoverImage { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}