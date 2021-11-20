namespace RobotWars.Logic
{
    public interface IRobotWarsGame
    {
        int ArenaHeight { get; }
        int ArenaWidth { get; }
        GameStatus GameStatus { get; }

        InstructionProcssingResult ProcessInstruction(string instruction);
    }
}