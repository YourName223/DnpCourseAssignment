using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = [];
    public PostInMemoryRepository()
    {
        //Dummy data
        AddAsync(new Post()
        {
            Title="A post",
            Body="This is a fine post",
            UserId = 0
        });
        AddAsync(new Post()
        {
            Title="A good post",
            Body="This is a great post",
            UserId = 1
        });
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()?posts.Max(p=>p.Id)+1:0;
        posts.Add(post);
        return Task.FromResult(post);
    }
    public Task UpdateAsync(Post post)
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
    public Task DeleteAsync(Post post)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == post.Id); 

        if (postToRemove is null) 
        { 
            throw new InvalidOperationException( $"Post with ID '{post.Id}' not found"); 
        } 
        
        posts.Remove(postToRemove); 
        return Task.CompletedTask;
    }
    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);

        if (post is null) 
        { 
            throw new InvalidOperationException( $"Post with ID '{id}' not found"); 
        } 

        return Task.FromResult(post);
    }
    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}