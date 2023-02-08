using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Zamat.Common.FilterQuery;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;
using Zamat.Sample.Services.Content.Core.Entities;
using Zamat.Sample.Services.Content.Core.Services;

namespace Zamat.Sample.Services.Content.Api.Rest.Controllers.Articles.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/articles")]
public class ArticlesController : ApiController
{
    private readonly ArticleService _articleService;
    private readonly IUuidGenerator _uuidGenerator;

    public ArticlesController(ArticleService articleService, IUuidGenerator uuidGenerator, IStringLocalizer<Translations> stringLocalizer, ILogger<ArticlesController> logger) : base(stringLocalizer, logger)
    {
        _articleService = articleService;
        _uuidGenerator = uuidGenerator;
    }

    [SwaggerOperation(
        Summary = "Create article",
        Description = "Create article",
        OperationId = "CreateArticle",
        Tags = new[] { "Articles" }
    )]
    [SwaggerResponse(201, "The article was created")]
    [SwaggerResponse(204, "The article was not created")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> CreateAsync(CreateArticleRequest request)
    {
        var article = new Article()
        {
            Id = _uuidGenerator.Generate(),
            Title = request.Title,
            Text = request.Text
        };

        var result = await _articleService.CreateAsync(article);
        if (!result.Succeeded)
        {
            _logger.LogWarning(ContentLogEvents.ArticleCreated, "Article cannot be created ({Errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(ContentLogEvents.ArticleCreated, "Article created (articleId : {Id})", article.Id);

        return CreatedAtRoute("GetArticle", new { article.Id }, new CreateArticleResponse(article));
    }

    [SwaggerOperation(
        Summary = "Get articles paginable list",
        Description = "Get articles paginable list",
        OperationId = "GetArticlesPaginableList",
        Tags = new[] { "Articles" }
    )]
    [SwaggerResponse(200, "The articles paginator")]
    [HttpGet]
    public async Task<ActionResult<GetArticlesResponse>> GetAsync([FromQuery] GetArticlesRequest request)
    {
        var filter = new FilterQuery<Article>(request.SortBy);
        filter.SortMap(SortBy.IdDesc, x => x.Id, true);
        filter.SortMap(SortBy.IdAsc, x => x.Id, false);

        var count = await _articleService.CountAsync(filter);

        if (count <= 0)
        {
            return GetEmptyResult();
        }

        var articles = _articleService.GetAsync(filter, request.Page, request.Limit);

        var items = new List<GetArticleResponse>();

        await foreach (var article in articles)
        {
            items.Add(new GetArticleResponse(article));
        }

        return Ok(new GetArticlesResponse(request.Page, request.Limit, count, items));

        GetArticlesResponse GetEmptyResult() => new(request.Page, request.Limit);
    }

    [SwaggerOperation(
        Summary = "Get article",
        Description = "Get article",
        OperationId = "GetArticle",
        Tags = new[] { "Articles" }
    )]
    [SwaggerResponse(200, "The article entity")]
    [SwaggerResponse(204, "The article entity not found")]
    [HttpGet("{id}", Name = "GetArticle")]
    public async Task<ActionResult<GetArticleResponse>> GetAsync(string id)
    {
        var article = await _articleService.GetAsync(id);
        if (article is null)
        {
            _logger.LogWarning(ContentLogEvents.ArticleFetchError, "Article not found (id: {id})", id);
            return NoContent();
        }

        return Ok(new GetArticleResponse(article));
    }

    [SwaggerOperation(
        Summary = "Delete article",
        Description = "Delete article",
        OperationId = "DeleteArticle",
        Tags = new[] { "Articles" }
    )]
    [SwaggerResponse(204, "The article entity was deleted.")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        var result = await _articleService.DeleteAsync(id);
        if (!result.Succeeded)
        {
            _logger.LogWarning(ContentLogEvents.ArticleDeleted, "Article cannot be deleted (errors: {errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(ContentLogEvents.ArticleDeleted, "Article was deleted (articleId : {Id})", id);

        return NoContent();
    }

    [SwaggerOperation(
        Summary = "Update article",
        Description = "Update article",
        OperationId = "UpdateArticle",
        Tags = new[] { "Articles" }
    )]
    [SwaggerResponse(204, "The article entity was updated.")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CreateArticleResponse>> UpdateAsync(string id, UpdateArticleRequest request)
    {
        var result = await _articleService.UpdateAsync(id, article =>
        {
            article.Title = request.Title;
            article.Text = request.Text;
        });

        if (!result.Succeeded)
        {
            _logger.LogWarning(ContentLogEvents.ArticleUpdated, "Article cannot be updated (errors: {errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(ContentLogEvents.ArticleUpdated, "Article updated (articleId : {Id}, request: {request})", id, request);

        return NoContent();
    }
}
