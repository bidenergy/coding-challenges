using System;

namespace RobotWars
{
    public class Arena
    {   
        public uint Length { get; private set; }
        public uint Width { get; private set; }

        // constructor

        public Arena(uint length, uint width )
        {
            Length = length;
            Width = width;

          //  Console.WriteLine($"Arena created Length: [{Length}] Width : [{Width}]");
        }
        public bool IsValid() 
        { 
           return  ((Length > 0)&& (Width > 0))?true:false;
        }

        public bool IsCoordsVaild(Coords coords)
        {
            return (coords.X <= Length) && (coords.Y <= Width);
        }
              
    }
}
