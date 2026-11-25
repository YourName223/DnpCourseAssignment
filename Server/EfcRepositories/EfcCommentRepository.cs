using RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Entities;
public class EfcCommentRepository : ICommentRepository 
{ 
    private readonly EfcRepositories.AppContext ctx; 
    public EfcCommentRepository(EfcRepositories.AppContext ctx) 
    { 
        this.ctx = ctx; 
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(p => p.Id == comment.Id))) 
        { 
            throw new Exception("Comment with id {comment.Id} not found"); 
        } 
        ctx.Comments.Update(comment); 
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"Comment with id {id} not found"); 
        } 
        ctx.Comments.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new Exception($"Comment with id {id} not found"); 
        }
        return existing;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }
}