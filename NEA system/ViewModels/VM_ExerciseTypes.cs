using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;
internal class VM_ExerciseTypes : VM_Base, IDataDisplay
{
    public ObservableCollection<ExerciseType> ExerciseTypes { get; set; }
    private ExerciseType selectedET;
    public ExerciseType SelectedET
    {
        get { return selectedET; }
        set
        {
            selectedET = value;

            //test
            if (selectedET != null)
            {
                System.Diagnostics.Debug.WriteLine("new selectedET name: " + selectedET.ExerciseTypeName);
            }
        }
    }



    public VM_ExerciseTypes()
    {
        ExerciseTypes = new();
    }



    public void LoadViewData()
    {
        ExerciseTypes.Clear();
        foreach (ExerciseType eT in Session.DB.Table<ExerciseType>())
        {
            ExerciseTypes.Add(eT);
        }
    }
}