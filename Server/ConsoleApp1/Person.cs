public class Person(string name)
{
    private string Name{get; set; } = name;

    public void Introduce()
    {
        Console.WriteLine($"Hello, im {Name}!");
    }
}