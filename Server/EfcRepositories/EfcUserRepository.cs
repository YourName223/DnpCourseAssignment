using RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Entities;
public class EfcUserRepository : IUserRepository 
{ 
    private readonly EfcRepositories.AppContext ctx; 
    public EfcUserRepository(EfcRepositories.AppContext ctx) 
    { 
        this.ctx = ctx; 
    }

    public async Task<User> AddAsync(User user)
    {
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(p => p.Id == user.Id))) 
        { 
            throw new Exception("User with id {user.Id} not found"); 
        } 
        ctx.Users.Update(user); 
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"User with id {id} not found"); 
        } 
        ctx.Users.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"User with id {id} not found"); 
        }
        return existing;
    }
    public async Task<User> GetSingleAsync(string username)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Username == username); 
        if (existing == null) 
        { 
            throw new Exception($"User with username {username} not found"); 
        }
        return existing;
    }

    public IQueryable<User> GetManyAsync()
    {
        return ctx.Users.AsQueryable();
    }
}