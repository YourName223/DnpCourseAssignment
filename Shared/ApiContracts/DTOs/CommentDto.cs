namespace DTOs;

public class CommentDto
{
    public string Body { get; set; }

    public int UserId {get; set;}
    public int PostId { get; set; }

    public CommentDto(string Body, int UserId, int PostId)
    {
        this.Body = Body;
        this.UserId = UserId;
        this.PostId = PostId;
    }
}