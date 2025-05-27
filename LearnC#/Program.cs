using System;
using System.Collections.Generic;

namespace LearnCS;

class Program
{
    static void Main()
    {
       
        bool close = true;
        while(close)
        {
            Console.Clear();
            Console.WriteLine("       Main menu       \n" +
                              "=======================\n" +
                              "[1] - My Lists menu\n" +
                              "[2] - My Pets menu\n" +
                              "[0] - Close");
            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    MyList.Menu();
                    break;

                case '2':
                    MyPets.StartGame();
                    break;

                case '0':
                    close = false;
                    break;
            }
        }
    }
}
