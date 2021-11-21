namespace RobotWars.Logic.Navigation
{
    public interface IRobotNavigator
    {
        PlanMoveResult PlanMove(Position position, RobotHeading robotHeading, RobotMove move);
        RobotHeading Rotate(RobotHeading robotHeading, RobotMove move);
    }
}
