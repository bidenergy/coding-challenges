using NUnit.Framework;
using RobotWars.Logic;
using RobotWars.Logic.Navigation;
using RobotWars.Logic.Parsing;

namespace RobotWars.Test
{
    public class RobotWarsGameTests
    {
        private RobotWarsGame _game;

        [SetUp]
        public void SetUp()
        {
            var inputParser = new InputParser();
            var robotNavigator = new RobotNavigator();
            _game = new RobotWarsGame(inputParser, robotNavigator);
        }

        [Test]
        public void Start_TestStartStates()
        {
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }

        [Test]
        public void Start_InvalidInput_NotInitialized()
        {
            var result = _game.ProcessInstruction("5");

            Assert.That(result.Successful, Is.False);
            Assert.That(result.FailureMessage, Is.Not.Null);

            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.Start));
            Assert.That(_game.ArenaWidth, Is.EqualTo(0));
            Assert.That(_game.ArenaHeight, Is.EqualTo(0));
        }

        [Test]
        public void PlaySampleInput_Works()
        {
            var result = _game.ProcessInstruction("5 4");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.Null);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddOrSelectRobot));
            Assert.That(_game.ArenaWidth, Is.EqualTo(5));
            Assert.That(_game.ArenaHeight, Is.EqualTo(4));


            result = _game.ProcessInstruction("1 2 N");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.Null);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.MoveRobot));
            Assert.That(_game.SelectedRobot, Is.EqualTo((Column: 1, Row: 2, Heading: RobotHeading.North)));


            result = _game.ProcessInstruction("LMLMLMLMM");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.EqualTo("1 3 N"));
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddOrSelectRobot));
            Assert.That(_game.SelectedRobot, Is.EqualTo((Column: 1, Row: 3, Heading: RobotHeading.North)));


            result = _game.ProcessInstruction("3 3 E");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.Null);
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.MoveRobot));
            Assert.That(_game.SelectedRobot, Is.EqualTo((Column: 3, Row: 3, Heading: RobotHeading.East)));


            result = _game.ProcessInstruction("MMRMMRMRRM");

            Assert.That(result.Successful, Is.True);
            Assert.That(result.SuccessMessage, Is.EqualTo("5 1 E"));
            Assert.That(result.FailureMessage, Is.Null);
            Assert.That(_game.GameStatus, Is.EqualTo(GameStatus.AddOrSelectRobot));
            Assert.That(_game.SelectedRobot, Is.EqualTo((Column: 5, Row: 1, Heading: RobotHeading.East)));
        }
    }
}
