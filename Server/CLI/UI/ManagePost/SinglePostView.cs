using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    
    public async Task Enter()
    {
        Console.WriteLine("To see post, enter post id, being number:");
        string? input = Console.ReadLine();
        if(int.TryParse(input, out int number))
        {
            await ViewPost(number);
        }
        else
        {
            Console.WriteLine("thats not a number....");
        }
    }

    public async Task ViewPost(int id)
    {
        Post post = await postRepository.GetSingleAsync(id);
        Console.WriteLine($"{post.Title} \n {post.Body}");
        Console.WriteLine("Comments on this post being:");
        foreach (Comment comment in commentRepository.GetManyAsync().Where(comment => comment.PostId == id))
        {
            Console.WriteLine($"{comment.Body}");
        }
    }
}