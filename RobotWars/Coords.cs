namespace RobotWars
{
    public class Coords
    {

        public uint X { get; set; }

        public uint Y { get; set; }

        // constructor

        public Coords(uint coordsX, uint coordsY)
        {
            this.X = coordsX;
            this.Y = coordsY;
        }
        public bool IsValid()
        {
            return ((this.X > 0) && (this.Y > 0)) ? true : false;
        }

    }
}
