// See https://aka.ms/new-console-template for more information

using System.ComponentModel;

Console.WriteLine("Press <Enter> to exit... ");
RobotWars game = new RobotWars();

Console.WriteLine(game.NextInstruction.GetEnumDescription());

string input = Console.ReadLine();
bool runGame = !string.IsNullOrEmpty(input);

//Run the game while the user hits the "Enter" key twice to exit
while (runGame)
{
    var validationResult = game.ProcessInput(input);
    
    if (!validationResult.Success)
        Console.WriteLine(validationResult.ErrorMessage); //Display error message if any
    else
        Console.WriteLine(validationResult.Message); //Display info message if any

    Console.WriteLine();
    Console.WriteLine(game.NextInstruction.GetEnumDescription()); //Display description for the instruction
    
    input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Press <Enter> again to exit... ");
        input = Console.ReadLine();
        
        if (string.IsNullOrEmpty(input))
            break;
    }
}

