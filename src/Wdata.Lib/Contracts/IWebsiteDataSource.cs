namespace Wdata.Contracts;

internal interface IWebsiteDataSource
{
    
    Task<string> FetchDataAsync(string path, CancellationToken cancel = default);
}