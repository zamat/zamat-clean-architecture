using Zamat.BuildingBlocks.Core;

namespace Zamat.Clean.Services.Users.Core.UnitOfWork;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IUserRepository UserRepository { get; }
}
