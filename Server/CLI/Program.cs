using CLI;
using InMemoryRepository;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUserRepository userRepository = new UserInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();

Console.WriteLine("test");

CLIApp cliApp = new CLIApp(userRepository, commentRepository, postRepository);
await cliApp.Start();