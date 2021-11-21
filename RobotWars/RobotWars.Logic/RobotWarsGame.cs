using RobotWars.Logic.Parsing;
using System;

namespace RobotWars.Logic
{
    public class RobotWarsGame : IRobotWarsGame
    {
        private readonly IInputParser _inputParser;

        #region Game State variables

        private GameStatus _gameStatus = GameStatus.Start;

        private ArenaCell[,] _arena;

        private SelectedRobot _selectedRobot;

        #endregion

        public int ArenaWidth => _arena?.GetLength(0) ?? 0;

        public int ArenaHeight => _arena?.GetLength(1) ?? 0;

        public GameStatus GameStatus => _gameStatus;

        public SelectedRobot SelectedRobot => _selectedRobot;

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
                case GameStatus.AddOrSelectRobot:
                    return ProcessAddOrSelectRobotInstruction(instruction);
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

        private InstructionProcssingResult ProcessAddOrSelectRobotInstruction(string instruction)
        {
            var parsed = _inputParser.ParseRobotAddOrSelectInput(instruction);
            if (parsed.ParseErrorMessage != null)
                return InstructionProcssingResult.InvalidInput(parsed.ParseErrorMessage);

            var row = parsed.Row;
            var column = parsed.Column;
            var heading = parsed.RobotHeading;

            var arenaCell = _arena[row, column];

            if (arenaCell == null)
            {
                _arena[row, column] = new ArenaCell(heading);
                _selectedRobot = new SelectedRobot { Row = row, Column = column, Heading = heading };
            }

            _gameStatus = GameStatus.MoveRobot;

            return InstructionProcssingResult.Success();
        }
    }
}
