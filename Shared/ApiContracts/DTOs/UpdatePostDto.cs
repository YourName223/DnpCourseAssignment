namespace DTOs;

public class UpdatePostDto
{
    public string Title { get; set; }
    public string Body { get; set; }

    public UpdatePostDto(string Title, string Body)
    {
        this.Title = Title;
        this.Body = Body;
    }
}