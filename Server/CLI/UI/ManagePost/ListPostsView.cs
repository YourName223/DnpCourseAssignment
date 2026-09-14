using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class ListPostView
{
    private readonly IPostRepository postRepository;

    public ListPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    public async Task Enter()
    {
        await ListPosts();
    }
    public async Task ListPosts()
    {
        Console.WriteLine("outputting all posts:");
        foreach (Post post in postRepository.GetManyAsync())
        {
            Console.WriteLine($"post: {post.Id} \n {post.Title}");
        }
    }
}