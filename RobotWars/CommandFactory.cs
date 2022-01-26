using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWars
{
    public class CommandFactory
    {
        public  CommandType CommandType { get; }
        public Arena Arena { get; set; }
        public Robot Robot { get; set; }

        public static ICommand GetExecuteCommand(string _command,CommandType CommandType, Arena arena, Robot robot) 
        {

            ICommand Command= null;
         
            if (CommandType == CommandType.ARENA)
            {

                Command = new ArenaCommand(_command);

            }
            else if (CommandType == CommandType.MOVE)
            {
              
                Command = new MoveCommand(_command, arena, robot);
            }
            else if (CommandType == CommandType.PLACE)
            {
                           
                Command = new PlaceCommand(_command, arena);

            }
            else
            {
                Console.WriteLine("invaild input , please read the instruction and try again");

            }
            return Command;
        }
    }
}
