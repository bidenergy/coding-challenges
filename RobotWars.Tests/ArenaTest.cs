using System;
using Xunit;
using Xunit.Abstractions;

namespace RobotWars.Tests
{

    public class ArenaTest
    {
        private readonly ITestOutputHelper _output;
        private Arena _arena;

        public ArenaTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreateArena_ReturnArena()
        {
            //Arrange
            uint length = 8;
            uint width = 6;

            //Act
            _arena = new Arena(length, width);

            //Assert
            Assert.Equal(length, _arena.Length);
            Assert.Equal(width , _arena.Width);

            _output.WriteLine($"Expected Arena Length : [{length}] actual : [{_arena.Length}]");
            _output.WriteLine($"Expected Arena Width : [{width}] actual : [{_arena.Width}]");
        }
        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1 ,2, true)]
          
        public void IsValidArena(uint length,uint width,bool isValid)
        {
            //Arrange


            //Act
            _arena = new Arena(length, width);

            bool actual= _arena.IsValid();

            //Assert
            Assert.Equal(isValid, actual);

            _output.WriteLine($"Expected Arena isValid : [{isValid}] actual : [{actual}]");
           
        }
        [Theory]
        [InlineData(3, 3, 3, 3 ,true)]
        [InlineData(1, 2, 0, 0 ,true)]
        [InlineData(1, 2, 3, 2, false)]

        public void IsCoordsVaild(uint length, uint width, uint x, uint y, bool isValid)
        {
            //Arrange
            Coords coords = new Coords(x,y);


            //Act
             _arena = new Arena(length, width);
            bool actual = _arena.IsCoordsVaild(coords);


            //Assert
            Assert.Equal(isValid, actual);

            _output.WriteLine($"Expected Arena isValid : [{isValid}] actual : [{actual}]");
        }
    }
}
