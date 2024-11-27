namespace Wdata.Models;

public class WebsitePost
{
    public string? Slug { get; set; } = null!;
    
    public string? Body { get; set; }

    public string? Title { get; set; } = null!;
    
    public string? Description { get; set; }

    public string? Summary { get; set; }
    
    public string? PostType { get; set; }
    
    public string? Author { get; set; }
    
    public string? Category { get; set; }
    
    public string?[]? Tags { get; set; }
    
    public string? CoverImage { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}