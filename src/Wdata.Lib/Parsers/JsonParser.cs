using System.Text.Json;

namespace Wdata.Parsers;

public class JsonParser<T>
{
    
    public T? Parse(string data)
    {
        if(string.IsNullOrWhiteSpace(data))
            throw new ArgumentNullException(nameof(data));
        
        return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
}