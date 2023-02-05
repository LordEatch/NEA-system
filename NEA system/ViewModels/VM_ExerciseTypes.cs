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

        //Query returns exercise types linked to a user via a record in the subscription table.
        string query = @$"
                SELECT DISTINCT ExerciseType.ExerciseTypeID, ExerciseType.ExerciseTypeName, ExerciseType.ExerciseTypeDescription
                FROM ExerciseType
                INNER JOIN Subscription ON Subscription.ExerciseTypeID = ExerciseType.ExerciseTypeID
                WHERE Subscription.UserID = '{Session.CurrentUser.UserID}'";

        //Populate the observable collection.
        foreach (ExerciseType eT in Session.DB.Query<ExerciseType>(query))
        {
            ExerciseTypes.Add(eT);
        }
    }
}