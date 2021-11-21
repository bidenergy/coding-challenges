namespace RobotWars.Logic.Navigation
{
    public class RobotNavigator : IRobotNavigator
    {
        public PlanMoveResult PlanMove(Position position, RobotHeading robotHeading, RobotMove move)
        {
            if (move == RobotMove.LeftRotate || move == RobotMove.RightRotate)
            {
                var nextHeading = Rotate(robotHeading, move);
                return PlanMoveResult.RotateResult(position, nextHeading);
            }
            else
            {
                var nextPosition = Move(position, robotHeading, move);
                return PlanMoveResult.MoveResult(nextPosition, robotHeading);
            }
        }

        public Position Move(Position position, RobotHeading robotHeading, RobotMove move)
        {
            if (move == RobotMove.LeftRotate || move == RobotMove.RightRotate)
                return position;

            Position nextPosition = position;

            switch (robotHeading)
            {
                case RobotHeading.North:
                    nextPosition = position with { Row = position.Row + 1 };
                    break;
                case RobotHeading.West:
                    nextPosition = position with { Column = position.Column - 1 };
                    break;
                case RobotHeading.South:
                    nextPosition = position with { Row = position.Row - 1 };
                    break;
                case RobotHeading.East:
                    nextPosition = position with { Column = position.Column + 1 };
                    break;
            }

            return nextPosition;
        }

        public RobotHeading Rotate(RobotHeading robotHeading, RobotMove move)
        {
            if (move == RobotMove.LeftRotate)
            {
                switch (robotHeading)
                {
                    case RobotHeading.North:
                        return RobotHeading.West;
                    case RobotHeading.West:
                        return RobotHeading.South;
                    case RobotHeading.South:
                        return RobotHeading.East;
                    case RobotHeading.East:
                        return RobotHeading.North;
                }
            }
            else if (move == RobotMove.RightRotate)
            {
                switch (robotHeading)
                {
                    case RobotHeading.North:
                        return RobotHeading.East;
                    case RobotHeading.West:
                        return RobotHeading.North;
                    case RobotHeading.South:
                        return RobotHeading.West;
                    case RobotHeading.East:
                        return RobotHeading.South;
                }
            }

            return robotHeading;
        }
    }
}
