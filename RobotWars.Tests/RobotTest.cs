using RobotWars.Enum;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class RobotTest
    {
        private readonly ITestOutputHelper _output;
        private Arena _arena;
        private Coords _coords;
        private Robot _robot;

    public RobotTest(ITestOutputHelper output)
        {
            _output = output;
              
        }

        [Fact]
        public void Exception_InvaildArena()
        {
            //Arrange
           // _arena = new Arena(0, 0);
            _coords = new Coords(6, 8);

            //Act
            var ex = Assert.Throws<NullReferenceException>(() => _robot = new Robot(_coords, Orientation.N, _arena));
            //Assert

            Assert.Equal("Invaild Arena", ex.Message);
        }
        [Fact]
        public void Exception_RobotPlacedOutsideArena()
        {
            //Arrange
            _arena = new Arena(6, 6);
            _coords = new Coords(6, 8);

            //Act
            var ex = Assert.Throws<InvalidOperationException>(() => _robot = new Robot(_coords, Orientation.N, _arena));
            //Assert

            Assert.Equal("Robot placed outside of Arena", ex.Message);
        }
        [Fact]
        public void Exception_RobotInvailedMove()
        {
            //Arrange
   
            _arena = new Arena(5, 5);
            _coords = new Coords(5, 5);

            //Act
            _robot = new Robot(_coords, Orientation.N, _arena);
          
            var ex = Assert.Throws<InvalidOperationException>(() =>  _robot.Move(ValidMove.M));
            //Assert

            Assert.Equal("Invaild Move", ex.Message);
        }

        [Theory]
        [InlineData("L", 1, 2, "N", "1 2 W")]
        [InlineData("M", 3, 3, "E", "4 3 E")]
        [InlineData("R", 3, 1, "E", "3 1 S")]
        public void SingleMoveCommands_ReturnPosition(string command, uint x, uint y, string orientation, string output)
        {
            //Arrange
            _arena = new Arena(5, 5);
            _coords = new Coords(x, y);
            Orientation orientationEnum = (Orientation)System.Enum.Parse(typeof(Orientation), orientation);

            //Act
            _robot = new Robot(_coords, orientationEnum, _arena); 
            _robot.Move((ValidMove)System.Enum.Parse(typeof(ValidMove), command.ToString()));


            string actualOutput = $"{_robot.Coords.X.ToString()} {_robot.Coords.Y.ToString()} {_robot.Orientation.ToString()}";
            //Assert

            Assert.Equal(output, actualOutput);

        }


        [Theory]
        [InlineData("LMLMLMLMM", 1 ,2 ,"N", "1 3 N")]
        [InlineData("MMRMMRMRRM",3 ,3, "E", "5 1 E")]
        public void MultiMoveCommands_ReturnPosition(string command, uint x, uint y, string orientation, string output)
        {
            //Arrange
            _arena = new Arena(5, 5);
            _coords = new Coords(x, y);
            Orientation orientationEnum = (Orientation)System.Enum.Parse(typeof(Orientation), orientation);

            //Act
            _robot = new Robot(_coords, orientationEnum, _arena);

            for (int currentMoveIndex = 0; currentMoveIndex < command.Length; currentMoveIndex++)
            {
                var currentMove = command[currentMoveIndex];
                ValidMove currentMoveCommand = (ValidMove)System.Enum.Parse(typeof(ValidMove), currentMove.ToString());
                _robot.Move(currentMoveCommand);
            }
           

            string actualOutput = $"{_robot.Coords.X.ToString()} {_robot.Coords.Y.ToString()} {_robot.Orientation.ToString()}";
            //Assert

            Assert.Equal(output, actualOutput);
          
        }

    }
}
