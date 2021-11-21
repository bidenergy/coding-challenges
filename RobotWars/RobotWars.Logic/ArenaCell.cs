namespace RobotWars.Logic
{
    public class ArenaCell
    {
        public RobotHeading? RobotHeading { get; private set; }

        public ArenaCell(RobotHeading robotHeading)
        {
            RobotHeading = robotHeading;
        }
    }
}
