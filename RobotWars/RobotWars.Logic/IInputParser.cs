namespace RobotWars.Logic
{
    public interface IInputParser
    {
        bool IsQuit(string input);
        InputResultArenaInit ParseArenaInitInput(string input);
    }
}
