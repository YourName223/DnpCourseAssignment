using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments = [];
    public CommentInMemoryRepository()
    {
        //Dummy data
        AddAsync(new Comment()
        {
            Body="This is a comment",
            PostId = 0,
            UserId = 0
        });
        AddAsync(new Comment()
        {
            Body="This is another comment",
            PostId = 1,
            UserId = 1
        });
        AddAsync(new Comment()
        {
            Body="This is a alot of comments",
            PostId = 0,
            UserId = 1
        });
    }
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()?comments.Max(p=>p.Id)+1:0;
        comments.Add(comment);
        return Task.FromResult(comment);
    }
    public Task UpdateAsync(Comment comment)
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
    public Task DeleteAsync(Comment comment)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == comment.Id); 

        if (commentToRemove is null) 
        { 
            throw new InvalidOperationException( $"Comment with ID '{comment.Id}' not found"); 
        } 
        
        comments.Remove(commentToRemove); 
        return Task.CompletedTask;
    }
    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);

        if (comment is null) 
        { 
            throw new InvalidOperationException( $"Comment with ID '{id}' not found"); 
        } 

        return Task.FromResult(comment);
    }
    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}