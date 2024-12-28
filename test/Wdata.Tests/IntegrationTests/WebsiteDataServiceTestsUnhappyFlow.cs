namespace Wdata.Tests.IntegrationTests;

public partial class WebsiteDataServiceTests
{

    [Fact]
    public async Task Get_non_existing_remote_data_should_throw_exception()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
            await _websiteDataService.GetWebsiteDataAsync("remote", "test/website-data1.json"));
    }

    [Fact]
    public async Task Get_non_existing_local_data_should_throw_exception()
    {
        var testDataPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "website-data-1.json");
        
        await Assert.ThrowsAsync<FileNotFoundException>(async () => 
            await _websiteDataService.GetWebsiteDataAsync("local", testDataPath));
    }
}