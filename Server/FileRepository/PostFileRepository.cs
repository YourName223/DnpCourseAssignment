using RepositoryContracts;
using Entities;
using System.Text.Json;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "post.json";
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        int maxID = posts.Count > 0 ? posts.Max(c => c.Id) : 1;
        post.Id = maxID + 1;
        posts.Add(post);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
        return post;
    }
    public async Task UpdateAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        int index = posts.FindIndex(c => c.Id == post.Id);
        if (index == -1)
        {
            return;
        }
        posts[index] = post;
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
    public async Task DeleteAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        int index = posts.FindIndex(c => c.Id == post.Id);
        if (index == -1)
        {
            return;
        }
        posts.RemoveAt(index);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        Post? post = posts.SingleOrDefault(c => c.Id == id);

        return post;
    }
    public IQueryable<Post> GetManyAsync()
    {
        string postsAsJson = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!.AsQueryable();
    }
}