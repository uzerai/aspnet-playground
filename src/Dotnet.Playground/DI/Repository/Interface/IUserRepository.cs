using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Repository.Interface;

public interface IUserRepository : IEntityRepository<User> { }
