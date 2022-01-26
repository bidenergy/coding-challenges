using System;

namespace RobotWars
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            Console.WriteLine("Robot War simulator in C# "+Environment.NewLine);
            var command = Console.ReadLine(); 
            Arena currentArena = null;
            Robot currentRobot = null;
               
            while (!exit)
            {  
                if (command == "exit")
                {
                    exit = true;
                    continue;
                }
                try
                {
                  
                    CommandReader commandReader = new CommandReader(command, currentArena, currentRobot);
                    commandReader.ExecuteCommand();

                    command = Console.ReadLine();
                    currentArena = commandReader.CommandReaderArena;
                    currentRobot = commandReader.CommandReaderRobot;
                }
                catch (Exception ex) 
                { 
                 Console.WriteLine(ex.Message);
                 command = Console.ReadLine();
                }

               

            }
        }

    }
    
}
