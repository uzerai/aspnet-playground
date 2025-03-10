
using Playground.Model.Authentication;

namespace Playground.DI.Repository;

public class UserRepository : IEntityRepository<User>
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User entity)
    {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdTrackingAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
