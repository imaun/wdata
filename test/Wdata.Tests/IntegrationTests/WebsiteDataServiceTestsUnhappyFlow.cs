namespace Wdata.Tests.IntegrationTests;

public partial class WebsiteDataServiceTests
{

    [Fact]
    public async Task Get_non_existing_remote_data_should_throw_exception()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
            await _websiteDataService.GetWebsiteDataAsync("remote", "test/website-data1.json"));
    }
}