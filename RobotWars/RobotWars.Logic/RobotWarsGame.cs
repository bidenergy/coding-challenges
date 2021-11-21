using RobotWars.Logic.Navigation;
using RobotWars.Logic.Parsing;
using System;
using System.Collections.Generic;

namespace RobotWars.Logic
{
    public class RobotWarsGame : IRobotWarsGame
    {
        private readonly IInputParser _inputParser;
        private readonly IRobotNavigator _robotNavigator;

        #region Game State variables

        private GameStatus _gameStatus = GameStatus.Start;

        private Stack<Robot> _robots = new Stack<Robot>();

        private int _arenaWidth = 0;

        private int _arenaHeight = 0;

        #endregion

        public int ArenaWidth => _arenaWidth;

        public int ArenaHeight => _arenaHeight;

        public GameStatus GameStatus => _gameStatus;

        public Robot SelectedRobot
        {
            get
            {
                if (_robots.Count == 0)
                    return null;
                else
                    return _robots.Peek();
            }
        }

        public RobotWarsGame(IInputParser inputParser, IRobotNavigator robotNavigator)
        {
            _inputParser = inputParser;
            _robotNavigator = robotNavigator;
        }

        public InstructionProcssingResult ProcessInstruction(string instruction)
        {
            switch (_gameStatus)
            {
                case GameStatus.Start:
                    return ProcessGameStartInstruction(instruction);
                case GameStatus.AddRobot:
                    return ProcessAddRobotInstruction(instruction);
                case GameStatus.MoveRobot:
                    return ProcessMoveRobotInstruction(instruction);
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

            _gameStatus = GameStatus.AddRobot;
            return InstructionProcssingResult.Success();
        }

        private InstructionProcssingResult ProcessAddRobotInstruction(string instruction)
        {
            var parsed = _inputParser.ParseRobotAddInput(instruction);
            if (parsed.ParseErrorMessage != null)
                return InstructionProcssingResult.InvalidInput(parsed.ParseErrorMessage);

            var row = parsed.Row;
            var column = parsed.Column;
            var heading = parsed.RobotHeading;

            var robot = new Robot(column, row, heading);
            
            _robots.Push(robot);

            _gameStatus = GameStatus.MoveRobot;

            return InstructionProcssingResult.Success();
        }

        private InstructionProcssingResult ProcessMoveRobotInstruction(string instruction)
        {
            var parsed = _inputParser.ParseRobotMoveInput(instruction);
            if (parsed.ParseErrorMessage != null)
                return InstructionProcssingResult.InvalidInput(parsed.ParseErrorMessage);

            var moves = parsed.RobotMoves;

            foreach (var move in moves)
            {
                var robot = _robots.Pop();

                var planMoveResult = _robotNavigator.PlanMove(robot, move);

                _robots.Push(planMoveResult.Robot);
            }

            _gameStatus = GameStatus.AddRobot;

            var robotForOutputMessage = _robots.Peek();
            var successMessasge = $"{robotForOutputMessage.Column} {robotForOutputMessage.Row} {robotForOutputMessage.Heading.ToCodeString()}";

            return InstructionProcssingResult.Success(successMessasge);
        }
    }
}
