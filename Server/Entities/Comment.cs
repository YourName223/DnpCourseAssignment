namespace Entities;

public class Comment
{
    public string Body{get; set; }
    public int Id{get; set; }
    public int UserId{get; set; }
    public int PostId{get; set; }

    public Comment(string Body, int UserId, int PostId)
    {
        this.Body = Body;
        this.UserId = UserId;
        this.PostId = PostId;
    }
}