public class GridSize
{
    public int MaxWidth { get; set; }
    public int MaxHeight { get; set; }
    public int MinWidth { get; set; }
    public int MinHeight { get; set; }

    public GridSize(int maxWidth, int maxHeight)
    {
        MaxWidth = maxWidth;
        MaxHeight = maxHeight;
        MinHeight = 0;
        MinWidth  = 0;
    }

    public GridSizeValidationResult Validate()
    {
        GridSizeValidationResult validationResult = new GridSizeValidationResult();
        
        if (MaxWidth < 0)
            validationResult.ErrorMessage = "Grid width must be greater than 0";
        else if (MaxHeight < 0)
            validationResult.ErrorMessage = "Grid height must be greater than 0";
        else if (MaxWidth == 1 && MaxHeight == 1)
            validationResult.ErrorMessage = "Grid cannot be 1 x 1";
        else
            validationResult.Success = true;

        return validationResult;
    }
}