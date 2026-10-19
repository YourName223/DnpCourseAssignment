using Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using System.Security;
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
        User user = new(request.UserName, request.PassWord);
        User created = await userRepo.AddAsync(user);
        UserDto dto = new()
        {
            Id = created.Id,
            UserName = created.UserName
        };
        return Created($"/users/{dto.Id}",created);
    }
}