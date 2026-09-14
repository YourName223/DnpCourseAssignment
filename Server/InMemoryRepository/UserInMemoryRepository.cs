using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = [];
    public UserInMemoryRepository()
    {
        //Dummy data
        AddAsync(new User()
        {
            UserName="Oscar",
            PassWord="12345"
        });
        AddAsync(new User()
        {
            UserName="BedreOscar",
            PassWord="12345678"
        });
    }
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()?users.Max(p=>p.Id)+1:0;
        users.Add(user);
        return Task.FromResult(user);
    }
    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id); 
        
        if (existingUser is null) 
        { 
            throw new InvalidOperationException($"User with ID '{user.Id}' not found"); 
        } 
        users.Remove(existingUser);

        users.Add(existingUser); 
        return Task.CompletedTask;
    }
    public Task DeleteAsync(User user)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == user.Id); 

        if (userToRemove is null) 
        { 
            throw new InvalidOperationException( $"User with ID '{user.Id}' not found"); 
        } 
        
        users.Remove(userToRemove); 
        return Task.CompletedTask;
    }
    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.Id == id);

        if (user is null) 
        { 
            throw new InvalidOperationException( $"User with ID '{id}' not found"); 
        } 

        return Task.FromResult(user);
    }
    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
}