using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class ArenaCommandTest
    {
        private readonly ITestOutputHelper _output;
        private const CommandType _commandType=CommandType.ARENA;
        private Arena _arena = null;
        private Robot _robot = null;
        public ArenaCommandTest(ITestOutputHelper output)
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

            Assert.Contains("[ERROR] in ExecuteArenaCommand Unable to ExecuteArenaCommand ", ex.Message);
        }

        [Fact]
        public void ExecuteArenaCommond_ReturnArena()
        {
            //Arrange
            string command = "5 6";

            ICommand commandObj = CommandFactory.GetExecuteCommand(command, _commandType, _arena, _robot);
            commandObj.CommandRobot = _robot;
            commandObj.CommandArena = _arena;

            //Act
             commandObj.Execute();

            //Assert

            Assert.Equal("5", commandObj.CommandArena.Length.ToString());
            Assert.Equal("6", commandObj.CommandArena.Width.ToString());
        }
    }
}
