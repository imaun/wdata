using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wdata.Configuration;
using Wdata.Contracts;
using Wdata.Extensions;

namespace Wdata.Tests.IntegrationTests;

public class WebsiteDataServiceTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IWebsiteDataService _websiteDataService;
    private readonly WdataConfig _config;

    public WebsiteDataServiceTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        var services = new ServiceCollection();
        services.AddWdata(configuration, "WebsiteData");
        services.AddWdataLocalSource();
        services.AddWdataRemoteSource();
        
        _serviceProvider = services.BuildServiceProvider();
        _websiteDataService = _serviceProvider.GetRequiredService<IWebsiteDataService>();
        _config = _serviceProvider.GetRequiredService<IOptions<WdataConfig>>().Value;
    }


    [Fact]
    public async Task Get_website_data_from_local_source()
    {
        var testDataPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "website-data.json");
        
        var result = await _websiteDataService.GetWebsiteDataAsync("local", testDataPath);

        Assert.NotNull(result);
        Assert.Equal("imun", result.Name);
    }
}