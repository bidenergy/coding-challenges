using RobotWars.Enum;
using RobotWars.Interfaces;
using System;
using System.IO;

namespace RobotWars
{
    public class Robot
    {

        public Coords Coords { get => _currentCoords; }
        
        public Orientation Orientation { get => _currentOrientation; }
      
        public Arena Arena { get; set; }
        private const int _numOrientations = 4;
        private Orientation _currentOrientation;
        private Coords _currentCoords;
        public Robot(Coords coords, Orientation orientation, Arena arena)
        {
            if ( arena != null) 
            {
                if (arena.IsCoordsVaild(coords))
                {
                    _currentCoords = coords;
                    _currentOrientation = orientation;
                    Arena = arena;
                }
                else
                {
                    throw new InvalidOperationException("Robot placed outside of Arena");
                }
            }
            else
            {
                throw new NullReferenceException("Invaild Arena");
            }

        }
 
        public void Report()
        {
            Console.WriteLine("-----------output------------------------");
            Console.WriteLine($"Robot Current Position {_currentCoords.X} {_currentCoords.Y} {_currentOrientation.ToString()}");

        }
        public void Move(Enum.ValidMove currentMoveCommand)
        {
            switch (currentMoveCommand)
            {
                
                case Enum.ValidMove.R:
                    _currentOrientation = (Orientation)System.Enum.ToObject(typeof(Orientation),
                        (((int)_currentOrientation + _numOrientations + 1) % _numOrientations));
                    break;
                case Enum.ValidMove.L:
                    _currentOrientation = (Orientation)System.Enum.ToObject(typeof(Orientation),
                        (((int)_currentOrientation + _numOrientations - 1) % _numOrientations));
                    break;
                case Enum.ValidMove.M:
                    MovewithMCommand();
                    break;
            }

        }

        private void MovewithMCommand() 
        {
            Coords newCoords;
            switch (_currentOrientation)
            {
                case Orientation.N:
                    newCoords = new Coords(_currentCoords.X, _currentCoords.Y + 1);
                    break;
                case Orientation.E:
                    newCoords = new Coords(_currentCoords.X + 1, _currentCoords.Y);
                    break;
                case Orientation.S:
                    newCoords = new Coords(_currentCoords.X, _currentCoords.Y - 1);
                    break;
                case Orientation.W:
                    newCoords = new Coords(_currentCoords.X - 1, _currentCoords.Y);
                    break;
                default:
                    newCoords = new Coords(0, 0);
                    break;
            }
            if (Arena.IsCoordsVaild(newCoords))
            {
                _currentCoords = newCoords;
            }
            else
            {
                throw new InvalidOperationException("Invaild Move");
            }

        }

    }
   
}
