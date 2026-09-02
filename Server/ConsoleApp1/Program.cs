Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World! v2");


void countClumps(int[] array)
{
    int clumps = 0;
    int currentNumber = 0;
    bool first = true;
    bool inClump = false;

    foreach(int number in array)
    {
        if(first)
        {
            currentNumber = number;
            first = false;
        }
        else
        {
            if(number == currentNumber && inClump == false)
            {
                clumps ++;
                inClump = true;
            }
            else
            {
                currentNumber = number;
                inClump = false;
            }
        }
    }

    Console.WriteLine(clumps);
}


countClumps([1,2,2,3,4,4]);