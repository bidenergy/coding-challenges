using System.Text.RegularExpressions;

namespace RobotWars.Logic
{
    public class InputParser : IInputParser
    {
        public bool IsQuit(string input)
        {
            return Regex.IsMatch(input, @"^\s*(q|quit|exit)\s*", RegexOptions.IgnoreCase);
        }

        public InputResultArenaInit ParseArenaInitInput(string input)
        {
            var match = Regex.Match(input, @"^\s*(\d+)\s+(\d+)\s*$");
            if (!match.Success)
            {
                return InputResultArenaInit.Invalid("Invalid input! Specify width and height of area like this (5 4)");
            }

            var width = int.Parse(match.Groups[1].Value);
            var height = int.Parse(match.Groups[2].Value);

            return new InputResultArenaInit { Width = width, Height = height };    
        }
    }
}
