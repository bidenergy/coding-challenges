using RobotWars.Interfaces;
using System;

namespace RobotWars
{
    public class ArenaCommand : ICommand
    {     
        public Arena CommandArena { get; set; }
        public Robot CommandRobot { get; set; }
        private string _command;

        public ArenaCommand(String command) {
             _command = command;
        }

        public void Execute() 
        {
            string[] arenaCommandParts = _command.Split(' ');
            try
            {
                uint length = uint.Parse(arenaCommandParts[0]);
                uint width = uint.Parse(arenaCommandParts[1]);
                CommandArena = new Arena(length, width);
            }
            catch (Exception ex)
            {
                throw new Exception($"[ERROR] in ExecuteArenaCommand Unable to ExecuteArenaCommand  [{_command}] {ex.Message}");
            }

        }

    }
}
