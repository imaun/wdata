namespace Wdata.Contracts;

public interface IWebsiteDataParser<T>
{

    T Parse(string data);
}