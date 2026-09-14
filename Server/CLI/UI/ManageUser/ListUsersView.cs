using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task Enter()
    {
        await ListUsers();
    }

    public async Task ListUsers()
    {
        Console.WriteLine("All users in the system:");
        foreach (User user in userRepository.GetManyAsync())
        {
            Console.WriteLine($"user: {user.UserName} \n {user.Id}");
        }
    }
}