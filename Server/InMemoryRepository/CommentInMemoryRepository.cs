using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class CommentInMemoryRepository : IPostRepository
{
    private List<Comment> comments{get; set;}

    Task<Comment> AddAsync(Comment comment)
    {
        comments.Id = comments.Any<>?comments.Max(p=>p.id)+1:0;
        comments.Add(comment);
        return Task.FromResult<>;
    }
    Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id); 
        
        if (existingComment is null) 
        { 
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found"); 
        } 
        comments.Remove(existingComment);

        comments.Add(existingComment); 
        return Task.CompletedTask;
    }
    Task DeleteAsync(Comment comment)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id); 

        if (postToRemove is null) 
        { 
            throw new InvalidOperationException( $"Comment with ID '{id}' not found"); 
        } 
        
        comments.Remove(commentToRemove); 
        return Task.CompletedTask;
    }
    Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.id == id);

        if (comment is null) 
        { 
            throw new InvalidOperationException( $"Comment with ID '{id}' not found"); 
        } 

        return Task.FromResult(comment);
    }
    IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}