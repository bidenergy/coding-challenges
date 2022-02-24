public class Robot
{
    private Position _currentPosition;
    /// <summary>
    /// Unique identifier for the robot
    /// </summary>
    public Guid Id { get; set; }

    private Position CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public Robot(Position currentPosition)
    {
        _currentPosition = currentPosition;
        this.Id = Guid.NewGuid();  //A unique identifier will be generated for each robot for identifying purposes
    }

    public Position GetCurrentLocation() => _currentPosition;

    public void SetLocation(int x, int y, BearingType bearing)
    {
        Position position = new Position(x, y, bearing);
        _currentPosition = position;
    }

    /// <summary>
    /// Takes in a command to move left/right and changes the bearing of the robot depending on the current bearing
    /// </summary>
    /// <param name="movementInstruction"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Rotate(MovementInstructionType movementInstruction)
    {
        switch (_currentPosition.BearingType)
        {
            case BearingType.North:
                _currentPosition.BearingType = movementInstruction switch
                {
                    MovementInstructionType.RotateLeft => BearingType.West,
                    MovementInstructionType.RotateRight => BearingType.East,
                    _ => throw new ArgumentOutOfRangeException(nameof(movementInstruction), movementInstruction, null)
                };
                break;
            case BearingType.East:
                _currentPosition.BearingType = movementInstruction switch
                {
                    MovementInstructionType.RotateLeft => BearingType.North,
                    MovementInstructionType.RotateRight => BearingType.South,
                    _ => throw new ArgumentOutOfRangeException(nameof(movementInstruction), movementInstruction, null)
                };
                break;
            case BearingType.South:
                _currentPosition.BearingType = movementInstruction switch
                {
                    MovementInstructionType.RotateLeft => BearingType.East,
                    MovementInstructionType.RotateRight => BearingType.West,
                    _ => throw new ArgumentOutOfRangeException(nameof(movementInstruction), movementInstruction, null)
                };
                break;
            case BearingType.West:
                _currentPosition.BearingType = movementInstruction switch
                {
                    MovementInstructionType.RotateLeft => BearingType.South,
                    MovementInstructionType.RotateRight => BearingType.North,
                    _ => throw new ArgumentOutOfRangeException(nameof(movementInstruction), movementInstruction, null)
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// Moves the robot one grid point forward towards the current bearing
    /// </summary>
    /// <param name="movementInstruction"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Move()
    {
        switch (_currentPosition.BearingType)
        {
            case BearingType.North:
                _currentPosition.Y++;
                break;
            case BearingType.East:
                _currentPosition.X++;
                break;
            case BearingType.South:
                _currentPosition.Y--;
                break;
            case BearingType.West:
                _currentPosition.X--;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}