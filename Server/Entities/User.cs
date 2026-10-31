namespace Entities;

using System.Text.Json.Serialization;

public class User
{
    public string Username{get; set; }
    public string Password{get; set; }
    public int Id{get; set; }

    [JsonConstructor]
    public User(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
    }

    public User(string Username, string Password, int Id)
    {
        this.Username = Username;
        this.Password = Password;
        this.Id = Id;
    }
}