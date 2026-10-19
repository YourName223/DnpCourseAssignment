namespace DTOs;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int AuthorUserId { get; set; }

    public CreatePostDto(string Title, string Body, int AuthorUserId)
    {
        this.Title = Title;
        this.Body = Body;
        this.AuthorUserId = AuthorUserId;
    }
}