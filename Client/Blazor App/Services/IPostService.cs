using DTOs;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);

    public Task<PostDto> GetPostAsync(GetPostDto request);

    public Task<List<PostDto>> GetPostsAsync(GetPostDto request);

    public Task<List<PostDto>> getAllPostsAsync();
}