namespace Wdata.Models;

public sealed class WebsitePost
{
    private const string _html_break = "<br>";
    
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
    
    public string? Ref { get; set; }
    
    public string? RefMode { get; set; }
    
    public void AddToBody(string content, bool useBreak = true)
    {
        if (useBreak)
        {
            Body = $"{Body}{_html_break}{content}";
        }
        
        Body += content;
    }
}