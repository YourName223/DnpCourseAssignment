namespace Entities;

public class Post
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int Id { get; set; }

    public int UserId {get; set;}
    public User User { get; set; } = null!;

    public List<Comment> Comments {get; set;} = [];
    public Post(String title, String body, int UserId)
    {
        this.Title = title;
        this.Body = body;
        this.UserId = UserId;
    }
    private Post(){}
}