using NUnit.Framework;
using RobotWars.Logic;

namespace RobotWars.Test
{
    [TestFixture]
    public class InputParserTest
    {
        [TestCase("q", ExpectedResult = true)]
        [TestCase("Q", ExpectedResult = true)]
        [TestCase("quit", ExpectedResult = true)]
        [TestCase("exit", ExpectedResult = true)]
        [TestCase("1 1", ExpectedResult = false)]
        public bool IsQuit_Test(string input)
        {
            IInputParser inputParser = new InputParser();
            return inputParser.IsQuit(input);
        }

        [TestCase("1", true, 0, 0)]
        [TestCase("5 4", false, 5, 4)]
        [TestCase("4 5", false, 4, 5)]
        [TestCase(" 4 5 ", false, 4, 5)]
        [TestCase(" 4  5 ", false, 4, 5)]
        public void ParseArenaInit_Test(string input, bool isParseErrorExpected, int expectedWidth, int expectedHeight)
        {
            IInputParser inputParser = new InputParser();

            var result = inputParser.ParseArenaInitInput(input);

            if (isParseErrorExpected)
                Assert.That(result.ParseErrorMessage, Is.Not.Null);
            Assert.That(result.Width, Is.EqualTo(expectedWidth));
            Assert.That(result.Height, Is.EqualTo(expectedHeight));
        }
    }
}
