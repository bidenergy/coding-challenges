using NUnit.Framework;
using RobotWars.Logic;
using RobotWars.Logic.Navigation;
using System.Collections.Generic;

namespace RobotWars.Test
{
    [TestFixture]
    public class RobotNavigatorTest
    {
        [TestCaseSource(nameof(PlanMoveTestCases))]
        public PlanMoveResult PlanMove(Position position, RobotHeading heading, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.PlanMove(position, heading, move);
        }

        private static IEnumerable<TestCaseData> PlanMoveTestCases()
        {
            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.LeftRotate)
                .Returns(new PlanMoveResult { MovedPosition = false, Position = new Position(1, 1), Heading = RobotHeading.West});
            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.RightRotate)
                .Returns(new PlanMoveResult { MovedPosition = false, Position = new Position(1, 1), Heading = RobotHeading.East });

            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Position = new Position(1, 2), Heading = RobotHeading.North });
            
            yield return new TestCaseData(new Position(0, 0), RobotHeading.West, RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Position = new Position(-1, 0), Heading = RobotHeading.West });
            yield return new TestCaseData(new Position(0, 0), RobotHeading.South, RobotMove.MoveOneStep)
                .Returns(new PlanMoveResult { MovedPosition = true, Position = new Position(0, -1), Heading = RobotHeading.South });
        }

        [TestCase(RobotHeading.North, RobotMove.MoveOneStep, ExpectedResult = RobotHeading.North)]
        [TestCase(RobotHeading.North, RobotMove.LeftRotate, ExpectedResult = RobotHeading.West)]
        [TestCase(RobotHeading.West, RobotMove.LeftRotate, ExpectedResult = RobotHeading.South)]        
        [TestCase(RobotHeading.South, RobotMove.LeftRotate, ExpectedResult = RobotHeading.East)]        
        [TestCase(RobotHeading.East, RobotMove.LeftRotate, ExpectedResult = RobotHeading.North)]
        [TestCase(RobotHeading.North, RobotMove.RightRotate, ExpectedResult = RobotHeading.East)]
        [TestCase(RobotHeading.East, RobotMove.RightRotate, ExpectedResult = RobotHeading.South)]
        [TestCase(RobotHeading.South, RobotMove.RightRotate, ExpectedResult = RobotHeading.West)]
        [TestCase(RobotHeading.West, RobotMove.RightRotate, ExpectedResult = RobotHeading.North)]        
        public RobotHeading Rotate(RobotHeading heading, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.Rotate(heading, move);
        }

        [TestCaseSource(nameof(MoveTestCases))]
        public Position Move(Position position, RobotHeading heading, RobotMove move)
        {
            var navigator = new RobotNavigator();
            return navigator.Move(position, heading, move);
        }

        private static IEnumerable<TestCaseData> MoveTestCases()
        {
            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.LeftRotate)
                .Returns(new Position(1, 1));
            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.RightRotate)
                .Returns(new Position(1, 1));

            yield return new TestCaseData(new Position(1, 1), RobotHeading.North, RobotMove.MoveOneStep)
                .Returns(new Position(1, 2));
            yield return new TestCaseData(new Position(1, 1), RobotHeading.West, RobotMove.MoveOneStep)
                .Returns(new Position(0, 1));
            yield return new TestCaseData(new Position(1, 1), RobotHeading.South, RobotMove.MoveOneStep)
                .Returns(new Position(1, 0));
            yield return new TestCaseData(new Position(1, 1), RobotHeading.East, RobotMove.MoveOneStep)
                .Returns(new Position(2, 1));
        }
    }
}
