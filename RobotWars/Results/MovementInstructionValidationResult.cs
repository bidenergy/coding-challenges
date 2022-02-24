public class MovementInstructionValidationResult : IInputValidationResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public ICollection<MovementInstructionType> MovementInstructions { get; set; }
}