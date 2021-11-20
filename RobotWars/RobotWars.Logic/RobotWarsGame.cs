using System;

namespace RobotWars.Logic
{
    public class RobotWarsGame : IRobotWarsGame
    {
        private readonly IInputParser _inputParser;

        private GameStatus _gameStatus = GameStatus.Start;

        private ArenaCell[,] _arena;

        public int ArenaWidth => _arena?.GetLength(0) ?? 0;

        public int ArenaHeight => _arena?.GetLength(1) ?? 0;

        public GameStatus GameStatus => _gameStatus;

        public RobotWarsGame(IInputParser inputParser)
        {
            _inputParser = inputParser;
        }

        public InstructionProcssingResult ProcessInstruction(string instruction)
        {
            switch (_gameStatus)
            {
                case GameStatus.Start:
                    return ProcessGameStartInstruction(instruction);
            }

            throw new NotImplementedException();
        }

        private InstructionProcssingResult ProcessGameStartInstruction(string instruction)
        {
            var parsed = _inputParser.ParseArenaInitInput(instruction);
            if (parsed.ParseErrorMessage != null)
                return InstructionProcssingResult.InvalidInput(parsed.ParseErrorMessage);

            _arena = new ArenaCell[parsed.Width, parsed.Height];

            _gameStatus = GameStatus.AddOrSelectRobot;

            return InstructionProcssingResult.Success();
        }
    }
}
