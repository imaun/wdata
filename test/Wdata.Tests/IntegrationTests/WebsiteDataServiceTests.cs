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
        Assert.Equal("https://imun.io", result.RootUrl);
        Assert.Equal("imun.io", result.Domain);
    }

    [Fact]
    public async Task Get_website_data_from_remote_source()
    {
        // var remoteDataPath = "https://raw.githubusercontent.com/imaun/website/refs/heads/master/test/website-data.json";
        
        var result = await _websiteDataService.GetWebsiteDataAsync("remote", "test/website-data.json");
        
        Assert.NotNull(result);
        Assert.Equal("imun", result.Name);
        Assert.Equal("https://imun.io", result.RootUrl);
        Assert.Equal("imun.io", result.Domain);
    }

    [Fact]
    public async Task Get_website_post_from_local_source()
    {
        var testDataPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "website-post-sample.md");
        
        var result = await _websiteDataService.GetWebsitePostAsync("local", testDataPath);
        Assert.NotNull(result);
        Assert.Equal("test-post", result.Slug);
        Assert.Equal("Test", result.Title);
    }
}