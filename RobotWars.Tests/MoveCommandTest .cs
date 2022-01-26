using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class MoveCommandTest
    {
        private readonly ITestOutputHelper _output;
        private const CommandType _commandType=CommandType.MOVE;
        private Arena _arena = null;
        private Robot _robot = null;
        public MoveCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

      
        [Fact]
        public void Exception_InvaildRobot()
        {
            //Arrange
            string command = "E";

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            var ex = Assert.Throws<Exception>(() => commandObj.Execute());

            //Assert

            Assert.Contains("Please enter command to place robot first", ex.Message);
        }
       
        [Fact]
        public void Exception_InvaildInput()
        {
            //Arrange
            string command = "E0000";
            _arena = new Arena(5, 5);
            Coords coords = new Coords(5, 5);
            _robot = new Robot(coords, Orientation.N, _arena);

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            var ex = Assert.Throws<Exception>(() => commandObj.Execute());

            //Assert

            Assert.Contains("[ERROR] in ExecuteMoveCommand Unable to ExecuteMoveCommand ", ex.Message);
        }


        [Fact]
        public void Exception_InvaildMove()
        {
            //Arrange
            string command = "M";
            _arena = new Arena(5, 5);
            Coords coords = new Coords(5, 5);
            _robot = new Robot(coords , Orientation.N, _arena);

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);       

            //Act
            var ex = Assert.Throws<Exception>(() => commandObj.Execute());

            //Assert

            Assert.Contains("Invaild Move", ex.Message);
        }
      

        [Fact]
        public void ExecuteMoveCommond_ReturnArenaAndRobot()
        {
            //Arrange
            string command = "M";
            _arena = new Arena(5, 5);
            Coords coords = new Coords(5, 5);
            _robot = new Robot(coords, Orientation.W, _arena);

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            commandObj.Execute();

            //Assert

            string actualRobotOutput = $"{ commandObj.CommandRobot.Coords.X.ToString()} { commandObj.CommandRobot.Coords.Y.ToString()} { commandObj.CommandRobot.Orientation.ToString()}";
            string actualArenaOutput = $"{ commandObj.CommandArena.Length.ToString()} {commandObj.CommandArena.Width.ToString()}";

            Assert.Equal($"{ _arena.Length.ToString()} {_arena.Width.ToString()}", actualArenaOutput);
            Assert.Equal("4 5 W", actualRobotOutput);
        }
    }
}
