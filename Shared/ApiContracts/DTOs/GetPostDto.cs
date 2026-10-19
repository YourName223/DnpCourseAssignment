namespace DTOs;

public class GetPostDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

    public GetPostDto(string Title, string Body, int AuthorUserId)
    {
        this.Title = Title;
        this.Body = Body;
        this.UserId = AuthorUserId;
    }
}