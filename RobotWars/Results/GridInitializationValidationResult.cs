public class GridInitializationValidationResult : IInputValidationResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public GridSize GridSize { get; set; }
}