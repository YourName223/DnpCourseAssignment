namespace Entities;

public class Comment
{
    public required string Body{get; set; }
    public int Id{get; set; }
    public required int UserId{get; set; }
    public required int PostId{get; set; }
}