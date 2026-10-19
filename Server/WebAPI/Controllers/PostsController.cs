using System.Collections;
using Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    [HttpPost]
    public async Task<IResult> AddPost(
        [FromBody] CreatePostDto requst,
        [FromServices] IUserRepository userRepo)
    {
        Post post = new(requst.Title,requst.Body,requst.AuthorUserId);

        Post created = await postRepo.AddAsync(post);

        return Results.Created($"/posts/{created.Id}",created);
    }

    [HttpPatch("{id:int}")]
    public async Task<IResult> UpdatePost(
        [FromRoute] int id,
        [FromBody] UpdatePostDto request)
    {
        Post postToUpdate = await postRepo.GetSingleAsync(id);

        postToUpdate.Title = request.Title;
        postToUpdate.Body = request.Body;

        await postRepo.UpdateAsync(postToUpdate);

        return Results.NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetPost([FromRoute] int id)
    {
        Post post = await postRepo.GetSingleAsync(id);

        GetPostDto dto = new GetPostDto(post.Title,post.Body,post.UserId);

        return Results.Ok(dto);
    }

    [HttpGet]
    public async Task<IResult> GetPosts(
        [FromQuery] string? titleContains = null,
        [FromQuery] int? userId = null)
    {
        IQueryable posts;

        if(titleContains is not null)
        {
            posts = postRepo.GetManyAsync().Where(p => p.Title.Contains(titleContains));
        }
        else if(userId is not null)
        {
            posts = postRepo.GetManyAsync().Where(p => p.UserId == userId);
        }
        else
        {
            return Results.BadRequest();
        }
        

        return Results.Ok(posts);
    }
}
