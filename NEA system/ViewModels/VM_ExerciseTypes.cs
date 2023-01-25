using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;
internal class VM_ExerciseTypes : VM_Base, IDatabaseOutput
{
    public ObservableCollection<ExerciseType> ExerciseTypes { get; set; }
    public ExerciseType SelectedET
    {
        get { return null; }
        set
        {
            //If a workout was selected...
            if (value != null)
                //Go to its page.
                Shell.Current.GoToAsync($"{nameof(Page_EditExerciseType)}", new Dictionary<string, object>() { ["ExerciseType"] = value });
        }
    }
    
    public Command GoToPage_EditExerciseType { get; }



    public VM_ExerciseTypes()
    {
        GoToPage_EditExerciseType = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_EditExerciseType)}"));

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