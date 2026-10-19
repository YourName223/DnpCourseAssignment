namespace Entities;

public class Post
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public Post(string Title, string Body, int UserId)
    {
        this.Title = Title;
        this.Body = Body;
        this.UserId = UserId;
    }
}