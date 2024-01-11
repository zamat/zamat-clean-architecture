using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Content.Infrastructure.EFCore;
using Zamat.Common.FilterQuery;
using Zamat.Clean.Services.Content.Core.Entities;
using Zamat.Clean.Services.Content.Core.Interfaces;

namespace Zamat.Clean.Services.Content.Infrastructure.Repositories;

class ArticleRepository : IArticleRepository
{
    private readonly ContentDbContext _dbContext;

    public ArticleRepository(ContentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Article article, CancellationToken cancellationToken)
    {
        _dbContext.Add(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Article article, CancellationToken cancellationToken)
    {
        _dbContext.Update(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Articles.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        if (entity is not null)
            _dbContext.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Article?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Articles.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public Task<int> CountAsync(FilterQuery<Article> filter, CancellationToken cancellationToken = default)
    {
        return _dbContext.Articles.CountAsync(filter.Expression, cancellationToken);
    }

    public IAsyncEnumerable<Article> GetAllAsync()
    {
        return _dbContext.Articles.AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Article> GetAllAsync(FilterQuery<Article> filter, int page, int limit, CancellationToken cancellationToken = default)
    {
        return _dbContext.Articles.Where(filter.Expression).AsAsyncEnumerable();
    }

    public Task<bool> CheckPredicateAsync(FilterQuery<Article> filter, CancellationToken cancellationToken = default)
    {
        return _dbContext.Articles.AnyAsync(filter.Expression, cancellationToken);
    }
}
