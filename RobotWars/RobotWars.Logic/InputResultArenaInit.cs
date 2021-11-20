using System;

namespace RobotWars.Logic
{
    public record InputResultArenaInit
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string ParseErrorMessage { get; set; }

        internal static InputResultArenaInit Invalid(string message)
        {
            return new InputResultArenaInit { ParseErrorMessage = message };
        }
    }
}