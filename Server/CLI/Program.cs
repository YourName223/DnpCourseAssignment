using CLI;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUserRepository userRepository = new UserFileRepository();
IPostRepository postRepository = new PostFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();

Console.WriteLine("test");

CLIApp cliApp = new CLIApp(userRepository, commentRepository, postRepository);
await cliApp.Start();