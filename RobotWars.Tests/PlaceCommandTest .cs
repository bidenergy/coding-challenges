using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class PlaceCommandTest
    {
        private readonly ITestOutputHelper _output;
        private const CommandType _commandType=CommandType.PLACE;
        private Arena _arena = null;
        private Robot _robot = null;
        public PlaceCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Exception_InvaildInput()
        {
            //Arrange
            string command = "-1012";

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            var ex =  Assert.Throws<Exception>(() => commandObj.Execute());
          
            //Assert

            Assert.Contains("[ERROR] in ExecutePlaceCommand Unable to ExecutePlaceCommand ", ex.Message);
        }
        [Fact]
        public void Exception_InvaildPosition()
        {
            //Arrange
            string command = "5 6 E";
            _arena = new Arena(5, 5);

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            var ex = Assert.Throws<Exception>(() => commandObj.Execute());

            //Assert

            Assert.Contains("Robot placed outside of Arena", ex.Message);
        }
        [Fact]
        public void Exception_InvaildArena()
        {
            //Arrange
            string command = "5 6 E";

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
            var ex = Assert.Throws<Exception>(() => commandObj.Execute());

            //Assert

            Assert.Contains("Invaild Arena", ex.Message);
        }

        [Fact]
        public void ExecutePlaceCommond_ReturnArenaAndRobot()
        {
            //Arrange
            string command = "5 6 E";
            _arena = new Arena(10,8);

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
             commandObj.Execute();

            //Assert

            string actualRobotOutput = $"{ commandObj.CommandRobot.Coords.X.ToString()} { commandObj.CommandRobot.Coords.Y.ToString()} { commandObj.CommandRobot.Orientation.ToString()}";
            string actualArenaOutput = $"{ commandObj.CommandArena.Length.ToString()} {commandObj.CommandArena.Width.ToString()}";

            Assert.Equal($"{ _arena.Length.ToString()} {_arena.Width.ToString()}", actualArenaOutput);
            Assert.Equal(command, actualRobotOutput);
        }
    }
}
