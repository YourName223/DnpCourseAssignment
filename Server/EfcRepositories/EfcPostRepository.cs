using RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Entities;
public class EfcPostRepository : IPostRepository 
{ 
    private readonly EfcRepositories.AppContext ctx; 
    public EfcPostRepository(EfcRepositories.AppContext ctx) 
    { 
        this.ctx = ctx; 
    }

    public async Task<Post> AddAsync(Post post)
    {
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id))) 
        { 
            throw new Exception("Post with id {post.Id} not found"); 
        } 
        ctx.Posts.Update(post); 
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"Post with id {id} not found"); 
        } 
        ctx.Posts.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"Post with id {id} not found"); 
        }
        return existing;
    }

    public IQueryable<Post> GetManyAsync()
    {
        return ctx.Posts.AsQueryable();
    }
}