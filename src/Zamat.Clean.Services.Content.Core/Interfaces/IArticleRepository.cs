using Zamat.Clean.Services.Content.Core.Entities;
using Zamat.Common.Crud;

namespace Zamat.Clean.Services.Content.Core.Interfaces;

public interface IArticleRepository : ICrudRepository<Article, string>
{
}
