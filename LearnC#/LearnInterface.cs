using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCS;

// Надо прописать каждому кровню редкости его диапозон параметров
public enum PetRarityLevel
{
    Common,
    UnCommon,
    Rare,
    Epic,
    Legendary
}

public enum PetType
{
    None,
    Cat,
    Dog
}

interface IPet
{
    PetType Type { get; set; }
    string Name { get; }
    string? Description { get; }
    PetRarityLevel Rarity { get; set; }

    // Счастье
    double Happiness { get; set; }
    double Independence { get; set; }
    const int maxHappiness = 100;
    const int minHappiness = 0;

    // Голод
    double Hunger { get; set; }
    double Gluttony { get; set; }
    const int maxHunger = 100;
    const int minHunger = 0;

    // Издать звук животного
    string MakeSound();

    // Пополнние голода
    public string GetEat()
    {
        double oldHunger = Hunger;
        Hunger += Math.Round(Gluttony * 20, 1);
        Hunger = Math.Clamp(Hunger, minHunger, maxHunger);
        double newHunger = Hunger;

        return $"{Name} had a delicious meal\n" +
               $"Hunger: {oldHunger} -> {newHunger}\n";
    }

    // Пополнение счастья
    public string PetIt()
    {
        double oldHappiness = Happiness;
        Happiness += Math.Round(Independence * 20, 1);
        Happiness = Math.Clamp(Happiness, minHappiness, maxHappiness);
        double newHappiness = Happiness;

        return $"{Name} be Pet\n"+
               $"Happiness: {oldHappiness} -> {newHappiness}\n";
    }

    // Получение информации о пете
    public string GetInfo()
    {
        return $"Name - {Name}  \n" +
               $"Rare - {Rarity}\n" +
               $"Happiness - {Happiness}/{IPet.maxHappiness}\n" +
               $"Hunger - {Hunger}/{IPet.maxHunger}\n" +
               $"Independence - {Independence}\n" +
               $"Gluttony - {Gluttony}\n" +
               $"======================\n" +
               $"Description:\n" +
               $"{Description}";
    }
}

interface IChampion
{
    int HP { get; set; }
    int Damage { get; set; }
}

interface IBusiness
{
    int GPM { get; set; }
}

class Cat : IPet
{
    public PetType Type { get; set; } = PetType.Cat;
    public string Name { get; }
    public string? Description { get; } = "It's a Cat. He (She) likes when you pet it.";
    public PetRarityLevel Rarity { get; set; }

    // Счастье
    public double Happiness { get; set; }
    public double Independence { get; set; }

    // Голод
    public double Hunger { get; set; }
    public double Gluttony { get; set; }

    public Cat(string name, PetRarityLevel rarityLevel, double independence, double gluttony)
    {
        Name = name ?? "Common Cat";
        Rarity = rarityLevel;
        Happiness = 50;
        Hunger = 50;
        Independence = independence;
        Gluttony = gluttony;
    }

    public string MakeSound()
    {
        return "Mew\n";
    }
}

class Dog : IPet
{
    public PetType Type { get; set; } = PetType.Dog;
    public string Name { get; }
    public string? Description { get; set; } = "It's a Dog. He (She) likes when you pet it.";
    public PetRarityLevel Rarity { get; set; }

    // Счастье
    public double Happiness { get; set; }
    public double Independence { get; set; }

    // Голод
    public double Hunger { get; set; }
    public double Gluttony { get; set; }

    public Dog(string name, PetRarityLevel rarityLevel, double independence, double gluttony)
    {
        Name = name ?? "Common Dog";
        Rarity = rarityLevel;
        Happiness = 50;
        Hunger = 50;
        Independence = independence;
        Gluttony = gluttony;
    }

    public string MakeSound()
    {
        return "Gaf\n";
    }
}

internal class MyPets
{
    static Dictionary<PetRarityLevel, (int chance, ((int minIndependence, int maxIndependence) Independence, (int minGluttony, int maxGluttony) Gluttony) IndAndGlu)> petsRarity = new Dictionary<PetRarityLevel, (int chance, ((int minIndependence, int maxIndependence) Independence, (int minGluttony, int maxGluttony) Gluttony) IndAndGlu)>(); // Тут храним редкости и их диапозон параметров


    static Dictionary<int, IPet> myPets = new Dictionary<int, IPet>(); // Тут храним всех наших петов

    public static void StartGame()
    {
        SetRarity();
        RunTimer();
        CreatePetFinal(PetType.Cat, "Barsik");
        CreatePetFinal(PetType.Dog, "Sharik");
        Menu();
    }


    // Модуль цикла (таймера) игры
    private static System.Timers.Timer? HungerTimer; // Это таймер
    public static void RunTimer()
    {
        HungerTimer = new System.Timers.Timer(600000);
        HungerTimer.Elapsed += OnTimedEvent;
        HungerTimer.AutoReset = true;
        HungerTimer.Enabled = true;
    }
    public static void OnTimedEvent(object? sender, EventArgs e)
    {
        foreach (var pet in myPets.Values)
        {
            pet.Hunger -= 1;
            pet.Hunger = Math.Clamp(pet.Hunger, IPet.minHunger, IPet.maxHunger);

            pet.Happiness -= 1;
            pet.Happiness = Math.Clamp(pet.Happiness, IPet.minHappiness, IPet.maxHappiness);
        }
    }


    public static void SetRarity()
    {
        petsRarity.Add(
            PetRarityLevel.Common, (
            45, // Шанс выпадения 45%
            (
                (1, 3), // min|max Independence
                (1, 3)  // min|max Gluttony
            )));
        petsRarity.Add(
            PetRarityLevel.UnCommon, (
            25, // Шанс выпадения 25%
            (
                (3, 4), // min|max Independence
                (3, 4)  // min|max Gluttony
            )));
        petsRarity.Add(
            PetRarityLevel.Rare, (
            15, // Шанс выпадения 15%
            (
                (4, 6), // min|max Independence
                (4, 6)  // min|max Gluttony
            )));
        petsRarity.Add(
            PetRarityLevel.Epic, (
            10, // Шанс выпадения 10%
            (
                (7, 9), // min|max Independence
                (7, 9)  // min|max Gluttony
            )));
        petsRarity.Add(
            PetRarityLevel.Legendary, (
            5, // Шанс выпадения 5%
            (
                (8, 11), // min|max Independence
                (8, 11)  // min|max Gluttony
            )));
    }


    public static void Menu()
    {

        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine("     My Pets menu     \n" +
                                "======================\n" +
                                "[1] - Check My Pets\n" +
                                "[0] - Back");
            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    CheckMyPets();
                    break;
                case '0':
                    youHere = false;
                    break;
            }
        }
    }

    public static void CheckMyPets()
    {
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine("   Check My Pets menu   \n" +
                                "===========================");
                
            var memoryDictionary = new Dictionary<int, int>();
            memoryDictionary.Clear();
            int count = 0;
            foreach(var Pet in myPets)
            {
                count++;
                memoryDictionary.Add(count, Pet.Key);
                Console.WriteLine($"[{count}] - {Pet.Value?.Name ?? "Uncnow"} - {Pet.Value?.Type}");
            }

            Console.WriteLine("===========================\n" +
                              "[Add] - Create New Animal\n" +
                              "[Back] - Back");

            var key = Console.ReadLine();
            switch (key?.ToLower())
            {
                case "add":
                    CreatePetStep1();
                    break;
                case "back":
                    youHere = false;
                    break;
                default:
                    if (int.TryParse(key, out int iD))
                    {
                        if (memoryDictionary.ContainsKey(iD)) CheckMyConcretPet(iD);
                    }
                    break;
            }
        }
    }
    public static void CheckMyConcretPet(int id)
    {
        var actions = new List<string>();
        var Pet = myPets[id];
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine($"       Pet menu     \n" +
                              $"======================");
            Console.WriteLine(Pet.GetInfo());
            Console.WriteLine($"======================\n" +
                                $"[1] - Pet\n" +
                                $"[2] - GetEat\n" +
                                $"[3] - MakeSound\n" +
                                $"[0] - Back");

            PrintActions(actions);

            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    actions.Add(Pet.PetIt());
                    break;
                case '2':
                    actions.Add(Pet.GetEat());
                    break;
                case '3':
                    actions.Add(Pet.MakeSound());
                    break;
                case '0':
                    youHere = false;
                    break;
            }
        }
    }

    // Тех модуль для CheckMyConcretPet
    public static void PrintActions(List<string> list)
    {
        Console.WriteLine($"\n        Actions       \n" +
                            $"======================\n");
        foreach (var action in list)
        {
            Console.WriteLine(action);
        }
    }

    // Модуль создания пета
    public static void CreatePetStep1()
    {
        var actions = new List <string>();
        PetType whoIs = PetType.None;
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine($"   Create Pet menu  \n" +
                              $"======================\n" +
                              $"Who is it?\n" +
                              $"======================\n" +
                              $"[1] - Cat\n" +
                              $"[2] - Dog\n" +
                              $"[3] - Random\n" +
                              $"[0] - Back\n");
            var key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    whoIs = PetType.Cat;
                    if (CreatePetStep2(whoIs)) youHere = false;
                    break;
                case '2':
                    whoIs = PetType.Dog;
                    if (CreatePetStep2(whoIs)) youHere = false;
                    break;
                case '3':
                    whoIs = RollPetType();
                    if (CreatePetStep2(whoIs)) youHere = false;
                    break;
                case '0':
                    youHere = false;
                    break;
                
            }
            
        }
    }
    public static bool CreatePetStep2(PetType whoIs)
    {
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine($"   Create Pet menu    \n" +
                              $"======================\n" +
                              $"[{whoIs}] | ???\n" +
                              $"======================\n" +
                              $"What is it called?\n" +
                              $"======================\n" +
                              $"Print the name..\n" +
                              $"[Back] - Back\n");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "back":
                    return false;
                default:
                    if (string.IsNullOrWhiteSpace(line)) break;
                    if (CreatePetStep3(whoIs, line)) return true;
                    break;
            }
        }
        return false;
    }
    public static bool CreatePetStep3(PetType whoIs, string name)
    {
        bool youHere = true;
        while (youHere)
        {
            Console.Clear();
            Console.WriteLine($"   Create Pet menu  \n" +
                              $"======================\n" +
                              $"[{whoIs}] | {name}\n" +
                              $"======================\n" +
                              $"Right?\n" +
                              $"======================\n" +
                              $"[1] - Yes\n" +
                              $"[0] - No");
            var key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    CreatePetFinal(whoIs, name);
                    return true;
                case '0':
                    return false;
            }
        }
        return false;
    }
    public static void CreatePetFinal(PetType type, string name)
    {
        IPet newAnimal;
        var rarity = RollRarityLevel();
        var Spe = RollSpecificationsLevel(rarity);

        switch (type)
        {
            case PetType.Dog:
                newAnimal = new Dog(name, rarity, Spe.independence, Spe.gluttony);
                myPets.Add(myPets.Count + 1, newAnimal);
                break;
            case PetType.Cat:
                newAnimal = new Cat(name, rarity, Spe.independence, Spe.gluttony);
                myPets.Add(myPets.Count + 1, newAnimal);
                break;
        }
    }

    // Бог рандома
    public static PetType RollPetType()
    {
        PetType typePet = PetType.None;
        bool justDoIt = true;
        while (justDoIt)
        {
            Random random = new Random();
            var values = Enum.GetValues(typeof(PetType));
            var diap = values.Length;
            var typePetObj = values.GetValue(random.Next(1, diap));
            if (typePetObj is null) continue;
            typePet = (PetType)typePetObj;
            if (typePet != PetType.None) justDoIt = false;
        }
        return typePet;
    }
    public static PetRarityLevel RollRarityLevel()
    {
        var random = new Random().Next(0, 100);
        int cumulativeChance = 0;
        foreach ( var value in petsRarity)
        {
            cumulativeChance += value.Value.chance;
            if (random < cumulativeChance)
            {
                return value.Key;
            }
        }
        return PetRarityLevel.Common;
    }
    public static (double independence, double gluttony) RollSpecificationsLevel(PetRarityLevel rarity)
    {
        double independence = 0;
        double gluttony = 0;

        var setRarity = petsRarity.TryGetValue(rarity, out var setRarityValue);

        int minInd = setRarityValue.IndAndGlu.Independence.minIndependence;
        int maxInd = setRarityValue.IndAndGlu.Independence.maxIndependence;

        int minGlu = setRarityValue.IndAndGlu.Gluttony.minGluttony;
        int maxGlu = setRarityValue.IndAndGlu.Gluttony.maxGluttony;

        var random = new Random();

        int setInd = random.Next(minInd, maxInd);
        int setGlu = random.Next(minGlu, maxGlu);

        independence = setInd / 10.0;
        gluttony = setGlu / 10.0;

        return (independence, gluttony);
    }
}

