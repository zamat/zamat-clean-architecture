using Zamat.Common.Crud;
using Zamat.Common.FilterQuery;
using Zamat.Sample.Services.Content.Core.Entities;
using Zamat.Sample.Services.Content.Core.Interfaces;

namespace Zamat.Sample.Services.Content.Core.Services;

public class ArticleService : CrudService<Article, string>
{
    public ArticleService(IArticleRepository repository) : base(repository)
    {
    }

    internal enum Errors
    {
        ArticleNotUnique
    }

    public override async Task<Result> ValidateAsync(Article entity, CancellationToken cancellationToken)
    {
        if (await CheckExistsAsync(entity, cancellationToken))
        {
            return new Result(new PreconditionError(Errors.ArticleNotUnique, "Article entity already exists"));
        }
        return new Result();
    }

    async Task<bool> CheckExistsAsync(Article entity, CancellationToken cancellationToken)
    {
        var filter = new FilterQuery<Article>();
        filter.Filter(f => f.Title == entity.Title);
        filter.Filter(f => f.Id != entity.Id);

        return await _repository.CheckPredicateAsync(filter, cancellationToken);
    }
}
