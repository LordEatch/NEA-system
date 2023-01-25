using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_Base, IRecordEditor
{
    public Workout MyWorkout { get; set; }
    public ObservableCollection<Exercise> Exercises { get; set; }
    private Exercise selectedExercise;
    public Exercise SelectedExercise
    {
        get { return selectedExercise; }
        set
        {
            selectedExercise = value;
            if (selectedExercise != null)
                Shell.Current.GoToAsync($"{nameof(Page_EditExercise)}", new Dictionary<string, object>() { ["Exercise"] = selectedExercise });
        }
    }

    public Command GoToPage_EditWorkout { get; }


    public VM_EditWorkout()
    {
        GoToPage_EditWorkout = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = MyWorkout }));

        Exercises = new();
    }



    public void LoadViewData()
    {
        //  Update exercises

        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            Exercises.Add(e);
        }



        //  Update the date

        OnPropertyChanged(nameof(MyWorkout));
    }

    public void SaveData()
    {
        Session.DB.Update(MyWorkout);
        System.Diagnostics.Debug.WriteLine($"Workout with id:{MyWorkout.WorkoutID} has been updated.");
    }

    public bool ValidateInputFormat()
    {
        //If any properties of the workout are null or empty (DateTime data type can never be null so a check is unecessary)...
        if (string.IsNullOrEmpty(MyWorkout.WorkoutMuscleGroup))
        {
            ErrorMessage = "Please enter a workout muscle group.";
            return false;
        }
        else
        {
            return true;
        }
    }
}