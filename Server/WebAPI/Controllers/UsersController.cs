using Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Microsoft.AspNetCore.Identity;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> addUser([FromBody] CreateUserDto request)
    {
        var hasher = new PasswordHasher<User>();
        User user = new User(request.Username, request.Password);
        user.Password = hasher.HashPassword(user,request.Password);
        User created = await userRepo.AddAsync(user);
        UserDto dto = new()
        {
            Id = created.Id,
            Username = created.Username
        };
        return Created($"/users/{dto.Id}", created);
    }
    [HttpGet("{id:int}")]
    public async Task<IResult> GetUser([FromRoute] int id)
    {
        User user = await userRepo.GetSingleAsync(id);

        if (user == null)
        {
            return Results.NotFound($"User with ID {id} not found");
        }

        UserDto dto = new()
        {
            Id = user.Id,
            Username = user.Username
        };
        return Results.Ok(dto);
    }
}