using System.Security.Claims;
using System.Text;
using Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace DNPProject.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IUserRepository userRepo;
    private readonly IConfiguration config;

    public AuthController(IConfiguration config, IUserRepository userRepo)
    {
        this.config = config;
        this.userRepo = userRepo;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLoginDto)
    {
        User? user = await ValidateUser(userLoginDto.Username, userLoginDto.Password);
        if (user == null)
        {
            // Don't reveal whether the username or the password was wrong
            return Unauthorized("Invalid username or password");
        }

        string token = GenerateJwt(user);
        return Ok(token);
    }

    private string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GenerateClaims(user)), // the payload
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256) // the signature
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }

    // The claims end up in the token's payload. The token is signed, NOT encrypted:
    // anyone holding it can read the claims, so never put secrets (like the password) in here.
    private static List<Claim> GenerateClaims(User user)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),           // who the token is about
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique id of this token
            new Claim("Id", user.Id.ToString())
        ];
    }

    public async Task<User?> ValidateUser(string username, string password)
    {
        var passwordHasher = new PasswordHasher<User>();

        User? user = await userRepo.GetSingleAsync(username);

        if (user is null)
            return null;

        var result = passwordHasher.VerifyHashedPassword
        (
            user,
            user.Password,
            password
        );

        if (result == PasswordVerificationResult.Failed)
            return null;

        return user;
    }
}
