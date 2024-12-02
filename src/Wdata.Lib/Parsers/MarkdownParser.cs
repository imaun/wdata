using System.Text.RegularExpressions;
using Markdig;

namespace Wdata.Parsers;

public partial class MarkdownParser
{
    private readonly YamlParser _yamlParser;
    
    public MarkdownParser()
    {
        _yamlParser = new YamlParser();
    }

    public (string body, Dictionary<string, object> metadata) Parse(string markdown)
    {
        var regex = front_matter_regex();
        var match = regex.Match(markdown);
        if (!match.Success)
        {
            var document = Markdig.Parsers.MarkdownParser.Parse(markdown);
            // TODO: log markdown does not contains valid yaml front matter.
            return (document.ToHtml(), new Dictionary<string, object>());
        }
        
        var yaml = match.Groups[1].Value;
        var body = match.Groups[2].Value;
        
        var metadata = _yamlParser.Parse(yaml);
        var bodyContent = Markdig.Parsers.MarkdownParser.Parse(body);
        return (bodyContent.ToHtml(), metadata);
    }


    public string ConvertToHtml(string markdown)
    {
        return Markdig.Parsers.MarkdownParser.Parse(markdown).ToHtml();
    }

    [GeneratedRegex(@"^---\s*\n(.*?)\n---\s*\n(.*)$", RegexOptions.Singleline)]
    private static partial Regex front_matter_regex();
}