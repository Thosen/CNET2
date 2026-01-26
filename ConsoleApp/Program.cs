// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// User will input numbers + enter. we will put there numbers. Once user presses Q + enter, we will output the sum of all numbers entered. 

double sum = 0;

Console.WriteLine("Enter numbers to add to the sum. Type 'Q' to quit.");

while(true)
{
    string input = Console.ReadLine();
    if(input.ToUpper() == "Q")
    {
        break;
    }
    if(double.TryParse(input, out double number))
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