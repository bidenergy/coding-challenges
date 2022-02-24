using System.ComponentModel;
using System.Text;

public class RobotWars
{
    public Instruction NextInstruction { get; set; }
    private GridSize GridSize { get; set; }
    private ICollection<Robot> Robots { get; set; }

    /// <summary>
    /// Initialize a new instance of the game
    /// Set the next expected instruction to setup the arena 
    /// </summary>
    public RobotWars()
    {
        NextInstruction = Instruction.InitializeGrid;
        Robots = new List<Robot>();
    }

    /// <summary>
    /// Take an input of a string and process according to the expected instruction type
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ProcessingResult ProcessInput(string input)
    {
        InputProcessor inputProcessor = new InputProcessor(input);
        ProcessingResult result = new ProcessingResult();
        
        if (inputProcessor.Validate())
        {
            switch (NextInstruction)
            {
                case Instruction.InitializeGrid:
                    var validationResult = inputProcessor.ProcessInitializeGridInstruction();
                    if (validationResult.Success)
                    {
                        GridSize = (validationResult as GridInitializationValidationResult).GridSize;
                        NextInstruction = Instruction.AddRobot; //Set the next instruction as to add a robot
                        result.Success = true;
                    }
                    else
                    {
                        result.ErrorMessage = validationResult.ErrorMessage;
                    }
                   
                    break;
                case Instruction.AddRobot:
                    validationResult = inputProcessor.ProcessAddInstruction();
                    if (validationResult.Success)
                    {
                        var position = (validationResult as RobotAddInstructionValidationResult).Position;
                        var addValidationResult = ValidateRobotAddInstruction(position);
                        if (addValidationResult.Success)
                        {
                            Robot robot = new Robot(position);
                            Robots.Add(robot);
                
                            NextInstruction = Instruction.MoveRobot; //Set the next instruction as to move the added robot     
                            result.Success = true;
                        }
                        else
                        {
                            result.ErrorMessage = addValidationResult.ErrorMessage;
                        }
                    }

                    break;
                case Instruction.MoveRobot:
                    var lastRobot = Robots.LastOrDefault(); //Move the robot that was added last
                    validationResult = inputProcessor.ProcessMoveInstruction();

                    if (validationResult.Success)
                    {
                        var moveInstructions = (validationResult as MovementInstructionValidationResult).MovementInstructions;
                        foreach (var instruction in moveInstructions)
                        {
                            switch (instruction)
                            {
                                case MovementInstructionType.RotateLeft:
                                case MovementInstructionType.RotateRight:
                                    lastRobot.Rotate(instruction);
                                    break;
                                case MovementInstructionType.MoveForward:
                                    lastRobot.Move();
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        
                        NextInstruction = Instruction.AddRobot; //Set the next command to add another robot

                        result.Message = EvaluateRobotPosition(lastRobot);
                        result.Success = true;
                    }
                    else
                    {
                        result.ErrorMessage = validationResult.ErrorMessage;
                    }
                    
                    break;
                default:
                    break;
            }
        }

        if (!result.Success)
        {
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                StringBuilder builder = new StringBuilder();
            
                builder.Append("Input is in incorrect format. Instructions can only be in the form of numbers and letters. Instructions must be in order to Set arena size > Add a robot > Move robot.");
                builder.AppendLine();
                builder.Append("Arena size example : 5 5");
                builder.AppendLine();
                builder.Append("Robot addition example : 1 2 N");
                builder.AppendLine();
                builder.Append("Robot move command example : LMLMLMLMMMM");

                result.ErrorMessage = builder.ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// Evaluates a given robot's position against the grid size and the other robots in the arena
    /// If the robot clashes with an existing robot in the arena, the existing robot will be removed from the arena
    /// If the robot moves out of the arena, the robot will be disqualified
    /// </summary>
    /// <param name="robot"></param>
    /// <returns></returns>
    private string EvaluateRobotPosition(Robot robot)
    {
        foreach (var robotInGrid in Robots.Where(x => x.Id != robot.Id))
        {
            if (robotInGrid.GetCurrentLocation().X == robot.GetCurrentLocation().X && robotInGrid.GetCurrentLocation().Y == robot.GetCurrentLocation().Y)
            {
                Robots.Remove(robotInGrid);
                return $"Your robot won over a robot in the grid. New Postion - {robot.GetCurrentLocation().X} {robot.GetCurrentLocation().Y} {robot.GetCurrentLocation().BearingType}";
            }
        }

        if (robot.GetCurrentLocation().X > GridSize.MaxWidth || robot.GetCurrentLocation().Y > GridSize.MaxHeight || robot.GetCurrentLocation().X < GridSize.MinWidth || robot.GetCurrentLocation().Y < GridSize.MinHeight)
        {
            Robots.Remove(robot);
            return "Your robot fell over the grid. Try again!";
        }

        return $"{robot.GetCurrentLocation().X} {robot.GetCurrentLocation().Y} {robot.GetCurrentLocation().BearingType}";
    }

    /// <summary>
    /// Validates the instruction given to add a robot against the grid size and the other robots in the arena
    /// A robot cannot be placed in the same cell as another robot
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private IInputValidationResult ValidateRobotAddInstruction(Position position)
    {
        IInputValidationResult inputValidationResult = new InputValidationResult();

        if (position.X > GridSize.MaxWidth)
        {
            inputValidationResult.ErrorMessage = $"Robot's X co-ordinate must be less than the grid width - {GridSize.MaxWidth}";
        }
        else if (position.Y > GridSize.MaxHeight)
        {
            inputValidationResult.ErrorMessage = $"Robot's Y co-ordinate must be less than the grid height - {GridSize.MaxHeight}";
        }
        else
        {
            if (Robots.Any(robot => robot.GetCurrentLocation().X == position.X && robot.GetCurrentLocation().Y == position.Y))
                inputValidationResult.ErrorMessage = $"There is another robot located in the position - ({position.X} x {position.Y})";
            else
                inputValidationResult.Success = true;
        }

        return inputValidationResult;
    }

    public GridSize GetGridSize() => GridSize;

    public ICollection<Robot> GetRobots() => Robots;
}

public enum MovementInstructionType
{
    RotateLeft,
    RotateRight,
    MoveForward
}

public enum BearingType
{
    North,
    East,
    South,
    West
}

public enum Instruction
{
    [Description("Enter command to add a robot in the format (X Y Direction) - Eg : 1 2 E")]
    AddRobot,
    [Description("Enter command to move the added robot in the format Eg : LMLMLMRRR")]
    MoveRobot,
    [Description("Enter command to setup the arena size in the format (MaxWidth x MaxHeight). Eg : 5 5")]
    InitializeGrid
}