// CNET2 console application

using ConsoleApp;
using ConsoleApp.Models;
using System.Text.Json;
using Model;
using Data;
using System.Threading.Channels;

Sandbox();
while (true)
{
}



static void Sandbox()
{
    


    //var dbContext = new PeopleDbContext();

    //var dummyAddress = new Address
    //{
    //    Street = "Test Street",
    //    City = "Test City",
    //    ZipCode = "00000"
    //};
    //Person osoba = new Person
    //{
    //    FirstName = "Jan",
    //    LastName = "Novak",
    //    DateOfBirth = new DateTime(1990, 5, 20),
    //    Email = "",
    //    Address = null,
    //};
    //dbContext.People.Add(osoba);
    //dbContext.SaveChanges();

    //var people_count = dbContext.People.Count();

    //Console.WriteLine("There are " + people_count + " people");
    //var osoba2rr = dbContext.People.ToList();
    //var osoba2 = dbContext.People.Where(osoba => osoba.Id == 9).Single();
    //osoba2.Email = "martina@seznam.cz";
    //dbContext.SaveChanges();
    //Console.WriteLine("Osoba 2 email: " + osoba2.Email);




}

static void CreateDb()
{
    var filePath = @"C:\Users\ageof\Downloads\data2024.json";
    var content = File.ReadAllText(filePath);
    var people = JsonSerializer.Deserialize<List<Person>>(content);
    var count = people.Count();
    Console.WriteLine($"People count: {count}");

    var dbContext = new PeopleDbContext();
    dbContext.People.AddRange(people);
    dbContext.SaveChanges();
    Console.WriteLine("People saved to database.");
}

static void DoPeopleStuff()
{
    var student = new Student
    {
        Surname = "  nOvAK  ",
        DateOfBirth = new DateOnly(2000, 01, 30),
        Trida = "9.B",
    };
    student.Adresa = new TextAddress
    {
        Ulice = "Hlavní",
        Mesto = "Brno",
        PSC = "11000"
    };
    var student2 = new Student
    {
        Surname = "  sVoboda  ",
        DateOfBirth = new DateOnly(2001, 05, 15),
        Trida = "9.A",
        Adresa = new RuianAdresa
        {
            NazevUlice = "Dlouhá",
            PSC = 73300,
            NazevObce = "Ostrava",
            CisloDomovni = 19,
        }
    };
    Console.WriteLine("Text address:");
    Console.WriteLine(student);
    Console.WriteLine("Ruian address");
    Console.WriteLine(student2);

    var teacher = new Teacher
    {
        Surname = "  kRAl  ",
        DateOfBirth = new DateOnly(1980, 03, 20),
        Payment = 35000,
    };
    Console.WriteLine("Teacher:");
    Console.WriteLine(teacher);
}

static void DoNumbersStuff()
{
    var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    for (int i = 0; i < numbers.Length; i++)
    {
        string numberString = GetNumberString(numbers[i]);
        Console.WriteLine($"{numbers[i]} -> {numberString}");
    }

    static string GetNumberString(int number)
    {
        var numbersDictionaryIntToString = new Dictionary<int, string>
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" }
    };

        return numbersDictionaryIntToString.TryGetValue(number, out var str) ? str : null;
    }
}

static void DoLinqStuff()
{
    int[] numbers = { 11, 2, 13, -97542, 44, -5, 6, 127, -99, 0, 256, 0, 12, 11 };

    // 1. how many positive numbers are in the array
    int positiveCount = numbers.Count(n => n > 0);
    Console.WriteLine($"Positive numbers count: {positiveCount}");

    // 2. how many negative numbers are in the array
    int negativeCount = numbers.Count(n => n < 0);
    Console.WriteLine($"Negative numbers count: {negativeCount}");

    // 3. sum of positive values
    int positiveSum = numbers.Where(n => n > 0).Sum();
    Console.WriteLine($"Sum of positive numbers: {positiveSum}");

    // 4. largest absolute value
    int maxAbs = numbers.Max(n => Math.Abs(n));
    Console.WriteLine($"Maximum absolute value: {maxAbs}");

    // 5. all positive even numbers
    var positiveEven = numbers.Where(n => n > 0 && n % 2 == 0).ToArray();
    Console.WriteLine("Positive even numbers: " + string.Join(", ", positiveEven));

    // 6. sort the array from smallest to largest values
    var sorted = numbers.OrderBy(n => n).ToArray();
    Console.WriteLine("Sorted numbers: " + string.Join(", ", sorted));

    // 7. skip the first 3 elements and sort the remaining values
    var skip3Sorted = numbers.Skip(3).OrderBy(n => n).ToArray();
    Console.WriteLine("Sorted after skipping first 3: " + string.Join(", ", skip3Sorted));



    // F R U I T S
    Console.WriteLine("\n---\n");
    var fruits = new[] { "aPPLE", "BlUeBeRrY", "cHeRry", "RaspbeRry" };

    // 1. Find out how many characters all words in the "fruits" array contain in total
    int totalChars = fruits.Sum(f => f.Length);
    Console.WriteLine($"Total number of characters in all fruits: {totalChars}");

    // 2. Lowercase all words
    var lowerFruits = fruits.Select(f => f.ToLower()).ToArray();
    Console.WriteLine("Fruits in lowercase: " + string.Join(", ", lowerFruits));

    // 3. Uppercase and lowercase (show both for each word)
    var upperAndLowerFruits = fruits.Select(f => $"Upper: {f.ToUpper()}, Lower: {f.ToLower()}").ToArray();
    Console.WriteLine("Fruits in upper and lower case:");
    foreach (var item in upperAndLowerFruits)
        Console.WriteLine(item);

    Console.WriteLine("\n---\n");
    // now with usage of UpperLowerString class
    var upperLowerFruits = fruits
        .Select(f => new UpperLowerString { UpperCase = f.ToUpper(), LowerCase = f.ToLower() })
        .ToArray();

    Console.WriteLine("Fruits using UpperLowerString class:");
    foreach (var item in upperLowerFruits)
        Console.WriteLine($"Upper: {item.UpperCase}, Lower: {item.LowerCase}");

    Console.WriteLine("\n---\n");
    //now with anonymous class
    var anonFruits = fruits
        .Select(f => new { UpperCase = f.ToUpper(), LowerCase = f.ToLower() })
        .ToArray();

    Console.WriteLine("Fruits using anonymous class:");
    foreach (var item in upperLowerFruits)
        Console.WriteLine($"Upper: {item.UpperCase}, Lower: {item.LowerCase}");

    Console.WriteLine("\n---\n");
    //now with touple
    var tupleFruits = fruits
        .Select(f => (UpperCase: f.ToUpper(), LowerCase: f.ToLower()))
        .ToArray();

    Console.WriteLine("Fruits using tuple:");
    foreach (var item in tupleFruits)
        Console.WriteLine($"Upper: {item.UpperCase}, Lower: {item.LowerCase}");
}

static void CalculateStuff()
{
    // User will input numbers + enter. we will put there numbers. Once user presses Q + enter, we will output the sum of all numbers entered. 
    double sum = 0;

    Console.WriteLine("Enter numbers to add to the sum. Type 'Q' to quit.");

    while (true)
    {
        string input = Console.ReadLine();
        if (input.ToUpper() == "Q")
        {
            break;
        }
        if (double.TryParse(input, out double number))
        {
            sum += number;
            Console.WriteLine($"Current sum: {sum}");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number or 'Q' to quit.");
        }
    }
    Console.WriteLine($"Total: {sum}");
}

static void ConvertStuff()
{
    while (true)
    {
        ConvertCtoF();
        Console.WriteLine("Do you want to convert another temperature? (Y/N)");
        string response = Console.ReadLine();
        if (response.ToUpper() != "Y")
        {
            break;
        }
    }

    // Input is temperature in C. Output is temperature in F.
    void ConvertCtoF()
    {
        Console.WriteLine("Enter temperature in Celsius:");
        string input = Console.ReadLine();
        if (double.TryParse(input, out double celsius))
        {
            double fahrenheit = celsius * 9 / 5 + 32;
            Console.WriteLine($"{celsius} C is {fahrenheit} F");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }
}

