namespace Wdata.Tests.IntegrationTests;

public partial class WebsiteDataServiceTests
{

    [Fact]
    public async Task Get_non_existing_remote_data_should_return_null()
    {
        var result = await _websiteDataService.GetWebsiteDataAsync("remote", "data/blog/heading.md");
        
        Assert.Null(result);
        // await Assert.ThrowsAsync<HttpRequestException>(async () =>
        //     await _websiteDataService.GetWebsiteDataAsync("remote", "data/blog/heading.md"));
    }

    [Fact]
    public async Task Get_non_existing_local_data_should_return_null()
    {
        var testDataPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "website-data-1.json");
        var result = await _websiteDataService.GetWebsiteDataAsync("local", testDataPath);
        
        Assert.Null(result);
    }
}