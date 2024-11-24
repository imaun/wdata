using System.Text.RegularExpressions;
using Markdig;

namespace Wdata.Parsers;

public class MarkdownParser
{
    private readonly YamlParser _yamlParser;
    
    public MarkdownParser()
    {
        _yamlParser = new YamlParser();
    }

    public (string, Dictionary<string, object>) Parse(string markdown)
    {
        var document = Markdig.Parsers.MarkdownParser.Parse(markdown);
        var regex = new Regex(@"^---\s*\n(.*?)\n---\s*\n(.*)$", RegexOptions.Singleline);
        var match = regex.Match(markdown);
        if (!match.Success)
        {
            // TODO: log markdown does not contains valid yaml front matter.
            return (document.ToHtml(), new Dictionary<string, object>());
        }
        
        var yaml = match.Groups[1].Value;
        var body = match.Groups[2].Value;
        
        var metadata = _yamlParser.Parse(yaml);
        return (body, metadata);
    }
}