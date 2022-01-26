using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace RobotWars
{
    public class CommandReader
    {
        public CommandType CommandType { get => _commandType;  }
        public Arena CommandReaderArena { get; set; }
        public Robot CommandReaderRobot { get; set; }

        private CommandType _commandType =  CommandType.INVAILED;
     
        private string _command { get; set; }

        private static readonly Regex _arenaRegex = new Regex(@"^[\d]+ [\d]$",RegexOptions.IgnoreCase);
        private static readonly Regex _placeRegex = new Regex(@"^[\d]+ [\d]+ [N|E|S|W]$", RegexOptions.IgnoreCase);
        private static readonly Regex _moveRegex = new Regex(@"^[MLR]+$", RegexOptions.IgnoreCase);

        public  CommandReader(string command, Arena arena, Robot robot)
        {
            _command = command;
            CommandReaderArena = arena;
            CommandReaderRobot = robot;
        
            SetCurrentCommandType();
            Console.WriteLine(_commandType.ToString()+" command: " + command);
            if (!IsValid()) 
            {
                throw new InvalidOperationException("Invaild input");
            }
        }
        public void ExecuteCommand()
        {  
            ICommand commandObj = CommandFactory.GetExecuteCommand(_command, _commandType, CommandReaderArena, CommandReaderRobot);
            commandObj.CommandRobot= CommandReaderRobot;
            commandObj.CommandArena= CommandReaderArena;
            commandObj.Execute();

            CommandReaderArena = commandObj.CommandArena;
            CommandReaderRobot = commandObj.CommandRobot;
        }
        public bool IsValid()
        {
            return (_commandType == CommandType.INVAILED) ? false : true;
        }
        public void SetCurrentCommandType()
        {
        
            if (_arenaRegex.IsMatch(_command))
            {
                _commandType=CommandType.ARENA;
            }
            else if (_placeRegex.IsMatch(_command))
            {
                _commandType = CommandType.PLACE;

            }
            else if (_moveRegex.IsMatch(_command))
            {
                _commandType = CommandType.MOVE;

            }

        }

    }
}

