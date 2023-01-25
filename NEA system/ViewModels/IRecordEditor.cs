namespace NEA_system.ViewModels;

internal interface IRecordEditor : IDatabaseInput, IDatabaseOutput
{
    public void SaveData();
}
