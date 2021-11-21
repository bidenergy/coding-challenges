namespace RobotWars.Logic.Parsing
{
    public interface IInputParser
    {
        bool IsQuit(string input);
        InputResultArenaInit ParseArenaInitInput(string input);
        InputResultRobotAddOrSelect ParseRobotAddOrSelectInput(string input);
    }
}
