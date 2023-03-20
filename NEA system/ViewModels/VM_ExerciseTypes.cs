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
    
    public Command GoToPage_CreateExerciseType { get; }



    public VM_ExerciseTypes()
    {
        GoToPage_CreateExerciseType = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_CreateExerciseType)}"));

        ExerciseTypes = new();
    }



    public void LoadViewData()
    {
        ExerciseTypes.Clear();

        //Populate the observable collection.
        foreach (ExerciseType eT in Session.GetSubscribedExerciseTypes())
        {
            ExerciseTypes.Add(eT);
        }
    }
}