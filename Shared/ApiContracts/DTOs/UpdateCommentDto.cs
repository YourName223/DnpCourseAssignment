namespace DTOs;

public class UpdateCommentDto
{
    public string Body { get; set; }

    public UpdateCommentDto(string Body)
    {
        this.Body = Body;
    }
}