using YamlDotNet.Serialization;

namespace Wdata.Parsers;

internal class YamlParser
{
    private readonly IDeserializer _deserializer;

    public YamlParser()
    {
        _deserializer = new DeserializerBuilder().Build();
    }

    public Dictionary<string, object> Parse(string content)
    {
        return _deserializer.Deserialize<Dictionary<string, object>>(content);
    }
}