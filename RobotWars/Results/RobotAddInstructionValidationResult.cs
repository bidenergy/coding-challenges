public class RobotAddInstructionValidationResult : IInputValidationResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public Position Position { get; set; }
}