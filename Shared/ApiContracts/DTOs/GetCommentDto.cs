namespace DTOs;

public class GetCommentDto
{
    public string Body { get; set; }
    public int UserId { get; set; }

    public GetCommentDto(string Body, int AuthorUserId)
    {
        this.Body = Body;
        this.UserId = AuthorUserId;
    }
}