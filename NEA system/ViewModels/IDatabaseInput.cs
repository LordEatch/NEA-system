namespace NEA_system.ViewModels;

internal interface IDatabaseInput
{
    //Interfaces must be public, even though I'd rather have this as protected.
    public bool ValidateInputFormat();
}
