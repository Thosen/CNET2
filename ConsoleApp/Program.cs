// CNET2 console application

using ConsoleApp;

DoLinqStuff();
while (true)
{
    

}

static void Sandbox()
{
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