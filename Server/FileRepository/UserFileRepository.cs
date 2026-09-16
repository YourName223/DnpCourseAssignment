using RepositoryContracts;
using Entities;
using System.Text.Json;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "user.json";
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        int maxID = users.Count > 0 ? users.Max(c => c.Id) : 1;
        user.Id = maxID + 1;
        users.Add(user);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return user;
    }
    public async Task UpdateAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        int index = users.FindIndex(c => c.Id == user.Id);
        if (index == -1)
        {
            return;
        }
        users[index] = user;
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
    }
    public async Task DeleteAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        int index = users.FindIndex(c => c.Id == user.Id);
        if (index == -1)
        {
            return;
        }
        users.RemoveAt(index);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
    }
    public async Task<User> GetSingleAsync(int id)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        User? user = users.SingleOrDefault(c => c.Id == id);

        return user;
    }
    public IQueryable<User> GetManyAsync()
    {
        string userAsJson = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<User>>(userAsJson)!.AsQueryable();
    }
}