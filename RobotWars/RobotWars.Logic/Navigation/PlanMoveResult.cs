namespace RobotWars.Logic.Navigation
{
    public record PlanMoveResult
    {
        public bool MovedPosition { get; set; }
        public Position Position { get; set; }
        public RobotHeading Heading { get; set; }

        public static PlanMoveResult RotateResult(Position position, RobotHeading heading)
        {
            return new PlanMoveResult { MovedPosition = false, Position = position, Heading = heading };
        }

        internal static PlanMoveResult MoveResult(Position position, RobotHeading heading)
        {
            return new PlanMoveResult { MovedPosition = true, Position = position, Heading = heading };
        }
    }
}
