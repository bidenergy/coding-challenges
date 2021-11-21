using RobotWars.Logic.Parsing;
using System;
using System.Collections.Generic;

namespace RobotWars.Logic
{
    public record Position
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }

    public class Robot
    {
        public RobotHeading Heading { get; set; }
    }

    public class RobotWarsGame : IRobotWarsGame
    {
        private readonly IInputParser _inputParser;

        #region Game State variables

        private GameStatus _gameStatus = GameStatus.Start;

        private Position _selectedPosition;

        private Dictionary<Position, Robot> _robots = new Dictionary<Position, Robot>();

        private int _arenaWidth = 0;

        private int _arenaHeight = 0;

        #endregion

        public int ArenaWidth => _arenaWidth;

        public int ArenaHeight => _arenaHeight;

        public GameStatus GameStatus => _gameStatus;

        public (int column, int row, RobotHeading robotHeading)? SelectedRobot
        {
            get
            {
                if (_selectedPosition != null && _robots.TryGetValue(_selectedPosition, out Robot robot))
                {
                    return (_selectedPosition.Column, _selectedPosition.Row, robot.Heading);
                } else
                {
                    return null;
                }
            }
        }

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

            _arenaWidth = parsed.Width;
            _arenaHeight = parsed.Height;

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

            var position = new Position { Column = column, Row = row };

            if (!_robots.ContainsKey(position))
            {
                var robot = new Robot { Heading = heading };
                _robots.Add(position, robot);
                _selectedPosition = position; 
            }

            _gameStatus = GameStatus.MoveRobot;

            return InstructionProcssingResult.Success();
        }
    }
}
