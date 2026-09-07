using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class UserInMemoryRepository : IPostRepository
{
    private List<User> users{get; set;}

    Task<User> AddAsync(User user)
    {
        users.Id = users.Any<>?users.Max(p=>p.id)+1:0;
        users.Add(user);
        return Task.FromResult<>;
    }
    Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == comment.Id); 
        
        if (existingUser is null) 
        { 
            throw new InvalidOperationException($"User with ID '{comment.Id}' not found"); 
        } 
        users.Remove(existingUser);

        users.Add(existingUser); 
        return Task.CompletedTask;
    }
    Task DeleteAsync(User comment)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == id); 

        if (userToRemove is null) 
        { 
            throw new InvalidOperationException( $"User with ID '{id}' not found"); 
        } 
        
        users.Remove(userToRemove); 
        return Task.CompletedTask;
    }
    Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.id == id);

        if (user is null) 
        { 
            throw new InvalidOperationException( $"User with ID '{id}' not found"); 
        } 

        return Task.FromResult(user);
    }
    IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
}