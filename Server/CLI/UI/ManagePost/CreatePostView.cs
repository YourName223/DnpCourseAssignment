using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task Enter()
    {
        Console.WriteLine("To create post, type title:");
        string? title = Console.ReadLine();
        Console.WriteLine("type body:");
        string? body = Console.ReadLine();
        Console.WriteLine("Type your user id:");
        string? input = Console.ReadLine(); 
        if(!int.TryParse(input, out int userId))
        {
            Console.WriteLine("thats not a number.... try everything again :D");
            return;
        }
        //Should make sure title/body is not null
        await CreatePostAsync(title, body, userId);
    }

    private async Task CreatePostAsync(string title, string body, int userId)
    {
        Post post = new Post()
        {
            Title=title,
            Body=body,
            UserId=userId
        };
        
        Post created = await postRepository.AddAsync(post);

        Console.WriteLine($"Post with the body: {created.Body} is created");
    }
}