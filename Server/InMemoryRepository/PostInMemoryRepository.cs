using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts{get; set;}

    Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any<>?posts.Max(p=>p.id)+1:0;
        posts.Add(post);
        return Task.FromResult<>;
    }
    Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id); 
        
        if (existingPost is null) 
        { 
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found"); 
        } 
        posts.Remove(existingPost);

        posts.Add(post); 
        return Task.CompletedTask;
    }
    Task DeleteAsync(Post post)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id); 

        if (postToRemove is null) 
        { 
            throw new InvalidOperationException( $"Post with ID '{id}' not found"); 
        } 
        
        posts.Remove(postToRemove); 
        return Task.CompletedTask;
    }
    Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.id == id);

        if (post is null) 
        { 
            throw new InvalidOperationException( $"Post with ID '{id}' not found"); 
        } 

        return Task.FromResult(post);
    }
    IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}