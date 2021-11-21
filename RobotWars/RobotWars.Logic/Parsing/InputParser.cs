using System;
using System.Text.RegularExpressions;

namespace RobotWars.Logic.Parsing
{
    public class InputParser : IInputParser
    {
        public bool IsQuit(string input)
        {
            return Regex.IsMatch(input, @"^\s*(q|quit|exit)\s*", RegexOptions.IgnoreCase);
        }

        public InputResultArenaInit ParseArenaInitInput(string input)
        {
            var match = Regex.Match(input, @"^\s*(\d+)\s+(\d+)\s*$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                var errorMessage = "Invalid input! Specify arena dimension as (width<int> height<int>). Eg: 5 4";
                return InputResultArenaInit.Invalid(errorMessage);
            }

            var width = int.Parse(match.Groups[1].Value);
            var height = int.Parse(match.Groups[2].Value);

            return new InputResultArenaInit { Width = width, Height = height };
        }

        public RobotHeading? ParseRobotHeading(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            return input.ToUpperInvariant() switch
            {
                "N" => RobotHeading.North,
                "W" => RobotHeading.West,
                "S" => RobotHeading.South,
                "E" => RobotHeading.East,
                _ => null,
            };
        }

        public InputResultRobotAddOrSelect ParseRobotAddOrSelectInput(string input)
        {
            const string inputInstruction = "Specify robot position as (column<int> row<int> heading<N,W,S,E>). Eg: 1 2 N";

            try
            {
                var match = Regex.Match(input, @"^\s*(\d+)\s+(\d+)\s+(N|W|S|E)\s*$", RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return InputResultRobotAddOrSelect.Invalid($"Invalid input! {inputInstruction}");
                }

                var column = int.Parse(match.Groups[1].Value);
                var row = int.Parse(match.Groups[2].Value);
                var robotHeading = ParseRobotHeading(match.Groups[3].Value).Value;

                return new InputResultRobotAddOrSelect { Column = column, Row = row, RobotHeading = robotHeading };
            }
            catch (Exception ex)
            {
                return InputResultRobotAddOrSelect.Invalid($"Error parsing input: {ex.Message}! {inputInstruction}");
            }
        }
    }
}
