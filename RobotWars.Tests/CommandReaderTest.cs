using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class CommandReaderTest
    {
        private readonly ITestOutputHelper _output;
      

        public CommandReaderTest(ITestOutputHelper output)
        {
            this._output = output;
        }
        [Fact]
        public void Exception_InvaildInput()
        {
            //Arrange
            string command = "hello";
            Arena arena = null;
            Robot robot = null;

            //Act
            var ex = Assert.Throws<InvalidOperationException>(() =>  new CommandReader(command, arena, robot));
            //Assert

            Assert.Equal("Invaild input", ex.Message);
        }

        [Theory]
        [InlineData("5 5", "ARENA")]
        [InlineData("1 2 N", "PLACE")]
        [InlineData("LMLMLMLMM", "MOVE")]
        [InlineData("3 3 E", "PLACE")]
        [InlineData("MMRMMRMRRM", "MOVE")]
        public void GetCommandType_ReturnCommandType(string command, string commandType)
        {
            //Arrange
            Arena arena = null;
            Robot robot = null;
            CommandReader commandReader = new CommandReader(command, arena, robot);
            //Act

            string expectedCommandType = commandReader.CommandType.ToString();

            //Assert

            Assert.Equal(expectedCommandType, commandType);
       
            _output.WriteLine($"Expected Current CommandType : [{expectedCommandType}] actual : [{commandType}]");

        }

      
    }
}
