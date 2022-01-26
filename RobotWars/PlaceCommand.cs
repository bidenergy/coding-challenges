using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWars
{
    public class PlaceCommand : ICommand
    {

       public Arena CommandArena { get; set; }
       public Robot CommandRobot { get; set; }
       private string _command;


        public PlaceCommand(string command, Arena Arena)
        {
            _command = command;
            CommandArena = Arena;
        }

        public void Execute() 
        {
            try
            {
                string[] placeCommandParts = _command.Split(' ');
                uint x = uint.Parse(placeCommandParts[0]);
                uint y = uint.Parse(placeCommandParts[1]);
                string orientation = placeCommandParts[2];
                Coords coords = new Coords(x, y);
                Orientation orientationEnum = (Orientation)System.Enum.Parse(typeof(Orientation), orientation);
                CommandRobot = new Robot(coords, orientationEnum, CommandArena);

            }
            catch (Exception ex)
            {
           
                throw new Exception($"[ERROR] in ExecutePlaceCommand Unable to ExecutePlaceCommand  [{_command}] {ex.Message}");
            }

        }
      // public void Create(string commandName, CommandType commondType);
    }
}
