namespace Entities;

public class User
{
    public string UserName{get; set; }
    public string PassWord{get; set; }
    public int Id{get; set; }

    public User(string UserName, string PassWord)
    {
        this.UserName = UserName;
        this.PassWord = PassWord;
    }
}