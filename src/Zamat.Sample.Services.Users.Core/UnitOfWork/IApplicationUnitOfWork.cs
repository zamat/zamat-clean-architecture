using Zamat.Sample.BuildingBlocks.Core;

namespace Zamat.Sample.Services.Users.Core.UnitOfWork;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IUserRepository UserRepository { get; }
}
