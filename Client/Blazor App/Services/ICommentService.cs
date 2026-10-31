using DTOs;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
}