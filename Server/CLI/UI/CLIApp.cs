using CLI.UI.ManagePost;
using CLI.UI.ManageUser;
using RepositoryContracts;

namespace CLI;

public class CLIApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
{
    public IUserRepository UserRepository { get; set; } = userRepository;
    public IPostRepository PostRepository { get; set; } = postRepository;
    public ICommentRepository CommentRepository { get; set; } = commentRepository;
    public async Task Start()
    {
        while (true)
        {
            Console.WriteLine("To do action, type and press enter: \n CP:Create post \n LP:ListPosts \n AC:Add Comment \n SP:Show post \n CU:Create user \n LU:List users");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "CP":
                    await new CreatePostView(postRepository).Enter();
                    break;
                case "LP":
                    await new ListPostView(postRepository).Enter();
                    break;
                case "AC":
                    await new ManagePostsView(postRepository, commentRepository).Enter();
                    break;
                case "SP":
                    await new SinglePostView(postRepository, commentRepository).Enter();
                    break;
                case "CU":
                    await new CreateUserView(userRepository).Enter();
                    break;
                case "LU":
                    await new ListUsersView(userRepository).Enter();
                    break;
                default:
                    Console.WriteLine("Input incorrect, try again");
                    break;
            }
        }
    }
}