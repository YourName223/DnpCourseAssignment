using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    
    public async Task Enter()
    {
        Console.WriteLine("To create comment, type body:");
        string? body = Console.ReadLine();
        Console.WriteLine("Type your user id");
        string? input = Console.ReadLine(); 
        if(!int.TryParse(input, out int userId))
        {
            Console.WriteLine("thats not a number.... try everything again :D");
            return;
        }
        Console.WriteLine("Type the post id");
        input = Console.ReadLine();
        if(!int.TryParse(input, out int postId))
        {
            Console.WriteLine("thats not a number.... try everything again :D");
            return;
        }
        //Should make sure body is not null
        await AddComment(body, userId, postId);
    }

    public async Task AddComment(string body, int userId, int postId)
    {
        Comment comment = new Comment()
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };

        Comment created = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment with the body: {created.Body} is created");
    }
}