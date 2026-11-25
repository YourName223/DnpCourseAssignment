namespace Entities;

public class Comment
{
    public string Body{get; set; }
    public int Id{get; set; }

    public int UserId {get; set; }
    public User User{get; set; } = null!;

    public int PostId{get; set; }
    public Post Post{get; set; } = null!;

    //Test, broken constructer, needs to use the user/post instead of id
    public Comment(string Body, int UserId, int PostId)
    {
        this.Body = Body;
        this.UserId = UserId;
        this.PostId = PostId;
    }

    private Comment(){}
}