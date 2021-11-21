using NUnit.Framework;
using RobotWars.Logic;
using RobotWars.Logic.Parsing;

namespace RobotWars.Test
{
    [TestFixture]
    public class InputParserTest
    {
        private IInputParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new InputParser();
        }

        [TestCase("q", ExpectedResult = true)]
        [TestCase("Q", ExpectedResult = true)]
        [TestCase("quit", ExpectedResult = true)]
        [TestCase("exit", ExpectedResult = true)]
        [TestCase("1 1", ExpectedResult = false)]
        public bool IsQuit_Test(string input)
        {
            return _parser.IsQuit(input);
        }

        [TestCase("1", true, 0, 0)]
        [TestCase("5 4", false, 5, 4)]
        [TestCase("4 5", false, 4, 5)]
        [TestCase(" 4 5 ", false, 4, 5)]
        [TestCase(" 4  5 ", false, 4, 5)]
        public void ParseArenaInit_Test(string input, bool expectParseError, int expectWidth, int expectHeight)
        {
            var result = _parser.ParseArenaInitInput(input);

            if (expectParseError)
            {
                Assert.That(result.ParseErrorMessage, Is.Not.Null);
            }
            else
            {
                Assert.That(result.ParseErrorMessage, Is.Null);
                Assert.That(result.Width, Is.EqualTo(expectWidth));
                Assert.That(result.Height, Is.EqualTo(expectHeight));
            }
        }

        [TestCase("", true, 0, 0, null)]
        [TestCase("1 2 n", false, 1, 2, RobotHeading.North)]
        [TestCase("1 2 N", false, 1, 2, RobotHeading.North)]
        [TestCase("2 1 W", false, 2, 1, RobotHeading.West)]
        [TestCase("2 1 S", false, 2, 1, RobotHeading.South)]
        [TestCase("2 1 E", false, 2, 1, RobotHeading.East)]
        public void ParseRoboAddOrSelect(string input, bool expectParseError, int expectColumn, int expectRow, RobotHeading? expectHeading)
        {
            var result = _parser.ParseRobotAddOrSelectInput(input);

            if (expectParseError)
            {
                Assert.That(result.ParseErrorMessage, Is.Not.Null);
            }
            else
            {
                Assert.That(result.ParseErrorMessage, Is.Null);
                Assert.That(result.Column, Is.EqualTo(expectColumn));
                Assert.That(result.Row, Is.EqualTo(expectRow));
                Assert.That(result.RobotHeading, Is.EqualTo(expectHeading));
            }
        }
    }
}
