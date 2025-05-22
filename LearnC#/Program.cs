using System;

namespace LearnCS;

class Program
{
    static void Main()
    {
        Console.WriteLine("1 = s");


        bool close = true;
        while(close)
        {
            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    Console.WriteLine("s");
                    break;

                case '0':
                    close = false;
                    break;
            }
        }
    }
}

class MyList
{

    static List<string> myList = new List<string>();

    public static void AddNewValue(string value)
    {
        myList.Add(value);
    }

    public static void CheckList()
    {
        foreach(string value in myList)
        {
            Console.WriteLine(value);
        }
    }

}
