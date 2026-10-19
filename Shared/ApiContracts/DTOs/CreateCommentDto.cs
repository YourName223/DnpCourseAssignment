namespace DTOs;

public class CreateCommentDto
{
    public string Body { get; set; }

    public int UserId {get; set;}
    public int PostId { get; set; }

    public CreateCommentDto(string Body, int UserId, int PostId)
    {
        this.Body = Body;
        this.UserId = UserId;
        this.PostId = PostId;
    }
}