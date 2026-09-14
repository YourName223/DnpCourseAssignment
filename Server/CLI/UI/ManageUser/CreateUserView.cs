using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task Enter()
    {
        Console.WriteLine("Creating user, type username:");
        string? username = Console.ReadLine();
        Console.WriteLine("Type password:");
        string? password = Console.ReadLine();
        //Should make sure username/password is not null
        await AddUserAsync(username, password);
    }

    private async Task AddUserAsync(string name, string password)
    {
        User user = new User()
        {
            UserName=name,
            PassWord=password
        };
        
        User created = await userRepository.AddAsync(user);

        Console.WriteLine($"User with the id: {created.Id} is created");
    }
}