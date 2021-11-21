namespace RobotWars.Logic
{
    public record SelectedRobot
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public RobotHeading Heading { get; set; }
    }
}
