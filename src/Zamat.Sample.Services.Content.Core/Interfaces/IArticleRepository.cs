using Zamat.Common.Crud;
using Zamat.Sample.Services.Content.Core.Entities;

namespace Zamat.Sample.Services.Content.Core.Interfaces;

public interface IArticleRepository : ICrudRepository<Article, string>
{
}
