public class Calculator
{
    public void add(int a, int b)
    {
        Console.WriteLine(a+b);
    }

    public void addList(int[] list)
    {
        int result = 0;

        foreach(int number in list)
        {
            result += number;
        }

        Console.WriteLine(result);
    }

    public void addFromConsole()
    {
        int first = Int32.Parse(Console.ReadLine());
        int second = Int32.Parse(Console.ReadLine());

        Console.WriteLine(first+second);
    }
}