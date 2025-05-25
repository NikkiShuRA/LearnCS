using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCS;
class MyList
{
    static Dictionary<string, List<string>> myLists = new Dictionary<string, List<string>>(); // Тут храним все наши листы

    public static void Menu()
    {
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine("    My Lists menu   \n" +
                              "====================\n" +
                              "[1] - Check мy Lists\n" +
                              "[0] - Back");
            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    CheckMyLists();
                    break;
                case '0':
                    youHere = false;
                    break;
            }
        }
    }

    public static void CheckMyLists()
    {
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine("    My Lists list   \n" +
                              "====================");
            var list = new Dictionary<int, string>();
            list.Clear();
            int count = 0;
            foreach (var value in myLists)
            {
                count++;
                list.Add(count, value.Key);
                Console.WriteLine($"[{count}] - {value.Key}");
            }
            Console.WriteLine("====================");
            Console.WriteLine("[Add] - AddNewList");
            Console.WriteLine("[Back] - Back");

            if (Console.ReadLine() is null) break;

            string? key = Console.ReadLine();

            switch (key?.ToLower())
            {
                case "add":
                    AddNewList();
                    break;
                case "back":
                    youHere = false;
                    break;
                default:
                    if (int.TryParse(key, out int result))
                    {
                        if (list.ContainsKey(result)) CheckConcretList(Convert.ToString(list[result]));
                    }
                    break;
            }
        }
    }
    public static void CheckConcretList(string listName)
    {
        myLists.TryGetValue(listName, out List<string>? list);
        if (list is null) return;

        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine($"My list '{listName}'\n" +
                               "====================");
            foreach (string value in list)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("====================");
            Console.WriteLine("[1] - AddNewValue");
            Console.WriteLine("[2] - ClearLastValue");
            Console.WriteLine("[3] - Delete this list");
            Console.WriteLine("[0] - Back");
            var key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    AddNewValue(list);
                    break;
                case '2':
                    if (list.Count > 0) list.RemoveAt(list.Count - 1);
                    break;
                case '3':
                    myLists.Remove(listName);
                    youHere = false;
                    break;
                case '0':
                    youHere = false;
                    break;
            }
        }
    }

    public static void AddNewList()
    {
        string? newList = "";
        bool youHere = true;
        while (youHere)
        {
            Console.WriteLine("Print name list");
            newList = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(newList)) youHere = false;
            Console.WriteLine("Pls rename");
        }
        if (String.IsNullOrEmpty(newList)) return;
        if (myLists.ContainsKey(newList)) return;
        var myList = new List<string>();
        myLists.Add(newList, myList);
    }
    public static void AddNewValue(List<string> list)
    {
        string? newValue = "";
        bool youHere = true;
        while (youHere)
        {
            Console.WriteLine("Print new value");
            newValue = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(newValue)) youHere = false;
        }
        if (newValue is null) return;
        list.Add(newValue);
    }
}

