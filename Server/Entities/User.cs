namespace Entities;

using System.Text.Json.Serialization;

public class User
{
    public string Username{get; set; }
    public string Password{get; set; }
    public int Id{get; set; }

    public List<Post> Posts {get; set; } = [];

    public List<Comment> Comments {get; set; } = [];

    [JsonConstructor]
    public User(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
    }

    private User(){}
}