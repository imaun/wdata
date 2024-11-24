using System.Text.Json;
using Wdata.Contracts;

namespace Wdata.Parsers;

public class WebsiteDataJsonParser<T> : IWebsiteDataParser<T>
{
    
    public T Parse(string data)
    {
        if(string.IsNullOrWhiteSpace(data))
            throw new ArgumentNullException(nameof(data));
        
        return JsonSerializer.Deserialize<T>(data)!;
    }
}