using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Repository;

public class ImageRepository : EntityRepository<Image>, IRepository<Image>
{
    public ImageRepository(DatabaseContext context) : base(context)
    {
    }
}