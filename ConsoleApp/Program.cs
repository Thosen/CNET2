// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// make a console app. user will input numbers + enter. we will put there numbers. Once user presses Q + enter, we will output the sum of all numbers entered. 

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