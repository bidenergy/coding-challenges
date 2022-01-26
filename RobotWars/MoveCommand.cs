using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWars
{
    public class MoveCommand: ICommand
    {
    

        public Arena CommandArena { get; set; }
        public Robot CommandRobot { get; set; }
        private string _command;

        public MoveCommand(string command, Arena Arena, Robot Robot)
        {
            _command = command;
            CommandRobot = Robot;
            CommandArena = Arena;
        }

        public void Execute() 
        {
            try
            {
                if (CommandRobot != null)
                {
                    for (int currentMoveIndex = 0; currentMoveIndex < _command.Length; currentMoveIndex++)
                    {
                        var currentMove = _command[currentMoveIndex];
                        Enum.ValidMove currentMoveCommand = (Enum.ValidMove)System.Enum.Parse(typeof(Enum.ValidMove), currentMove.ToString());
                        CommandRobot.Move(currentMoveCommand);
                        CommandRobot.Report();
                    }
      
                }
                else
                {
                    throw new InvalidOperationException($"Please enter command to place robot first");
                }

            }
            catch (Exception ex)
            {              
                throw new Exception($"[ERROR] in ExecuteMoveCommand Unable to ExecuteMoveCommand  [{_command}] {ex.Message}");
            }

        }
      // public void Create(string commandName, CommandType commondType);
    }
}
