using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Repository.Interface;

public interface IUserRepository : IEntityRepository<User> { }
