using System.Net;
using Zamat.Clean.Services.Content.Api.Rest.IntegrationTests;
using Zamat.Clean.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

namespace Zamat.Clean.Services.Content.Api.Rest.IntegrationTests.Controllers.v1.Articles;

public class ArticlesControllerTests : IClassFixture<ContentWebApplicationFactory>
{
    private readonly ContentWebApplicationFactory _factory;

    public ArticlesControllerTests(ContentWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/v1/articles")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        #region Arrange
        var client = _factory.CreateClient();
        #endregion

        #region Act
        var response = await client.GetAsync(url);
        #endregion

        #region Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.OK);
        #endregion
    }

    [Theory]
    [InlineData("Sample article", "Sample article text")]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string title, string text)
    {
        #region Arrange
        var client = _factory.CreateClient();
        #endregion

        #region Act
        var article = new CreateArticleRequest()
        {
            Title = title,
            Text = text
        };
        var response = await client.PostAsJsonAsync("/v1/articles", article);
        #endregion

        #region Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.Created);
        #endregion
    }
}
