using System.Diagnostics;
using CLI.UI.ManagePost;
using CLI.UI.ManageUser;
using RepositoryContracts;

namespace CLI;

public class CLIApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
{
    public IUserRepository UserRepository { get; set; } = userRepository;
    public IPostRepository PostRepository { get; set; } = postRepository;
    public ICommentRepository CommentRepository { get; set; } = commentRepository;

    private CreatePostView createPostView = new CreatePostView(postRepository);
    private ListPostView listPostView = new ListPostView(postRepository);
    private ManagePostsView managePostsView = new ManagePostsView(postRepository,commentRepository);
    private SinglePostView singlePostView = new SinglePostView(postRepository,commentRepository);
    private CreateUserView createUserView = new CreateUserView(userRepository);
    private ListUsersView listUsers = new ListUsersView(userRepository);

    public async Task Start()
    {
        while (true)
        {
            Console.WriteLine("To do action, type and press enter: \n CP:Create post \n LP:ListPosts \n AC:Add Comment \n SP:Show post \n CU:Create user \n LU:List users");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "CP":
                    await createPostView.Enter();
                    break;
                case "LP":
                    await listPostView.Enter();
                    break;
                case "AC":
                    await managePostsView.Enter();
                    break;
                case "SP":
                    await singlePostView.Enter();
                    break;
                case "CU":
                    await createUserView.Enter();
                    break;
                case "LU":
                    await listUsers.Enter();
                    break;
                default:
                    Console.WriteLine("Input incorrect, try again");
                    break;
            }
        }
    }
}