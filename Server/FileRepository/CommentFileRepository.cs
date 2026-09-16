using RepositoryContracts;
using Entities;
using System.Text.Json;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comment.json";

    public CommentFileRepository()
    {
        if(!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        int maxID = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxID + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if(index == -1)
        {
            return;
        }
        comments[index] = comment;
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    public async Task DeleteAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if(index == -1)
        {
            return;
        }
        comments.RemoveAt(index);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);

        return comment;
    }
    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!.AsQueryable();
    }
}