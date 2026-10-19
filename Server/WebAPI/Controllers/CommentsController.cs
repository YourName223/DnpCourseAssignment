using System.Collections;
using Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    [HttpPost]
    public async Task<IResult> AddComment(
        [FromBody] CreateCommentDto requst,
        [FromServices] ICommentRepository commentRepo)
    {
        Comment comment = new(requst.Body,requst.UserId,requst.PostId);

        Comment created = await commentRepo.AddAsync(comment);

        return Results.Created($"/posts/{created.Id}",created);
    }

    [HttpPatch("{id:int}")]
    public async Task<IResult> UpdateComment(
        [FromRoute] int id,
        [FromBody] UpdateCommentDto request)
    {
        Comment commentToUpdate = await commentRepo.GetSingleAsync(id);

        commentToUpdate.Body = request.Body;

        await commentRepo.UpdateAsync(commentToUpdate);

        return Results.NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetComment([FromRoute] int id)
    {
        Comment? comment = await commentRepo.GetSingleAsync(id);

        if (comment is null)
        {
            return Results.NotFound($"Comment with ID {id} was not found.");
        }

        GetCommentDto dto = new GetCommentDto(comment.Body, comment.UserId);

        return Results.Ok(dto);
    }

    [HttpGet]
    public async Task<IResult> GetComments(
        [FromQuery] int? postId = null,
        [FromQuery] int? userId = null)
    {
        IQueryable posts;

        if(postId is not null)
        {
            posts = commentRepo.GetManyAsync().Where(p => p.PostId == postId);
        }
        else if(userId is not null)
        {
            posts = commentRepo.GetManyAsync().Where(p => p.UserId == userId);
        }
        else
        {
            return Results.BadRequest();
        }
        

        return Results.Ok(posts);
    }
}
