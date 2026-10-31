namespace DTOs;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public List<CommentDto> CommentDtos {get; set;}

    public PostDto(int Id, string Title, string Body, int UserId, List<CommentDto> commentDtos)
    {
        this.Id = Id;
        this.Title = Title;
        this.Body = Body;
        this.UserId = UserId;
        this.CommentDtos = commentDtos;
    }
}