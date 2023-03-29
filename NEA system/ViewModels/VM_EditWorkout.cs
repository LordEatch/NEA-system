using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_Base, IRecordEditor
{
    public Workout MyWorkout { get; set; }
    //Exclusive WorkoutDate property used to convert DateTime in the date picker to a short date string.
    public DateTime WorkoutDate
    {
        set
        {
            MyWorkout.Date = value.ToShortDateString();
        }
    }
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

    public Command GoToPage_CreateExercise { get; set; }
    public Command DeleteWorkoutCommand { get; set; }



    public VM_EditWorkout()
    {
        GoToPage_CreateExercise = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_CreateExercise)}", new Dictionary<string, object>() { ["WorkoutID"] = MyWorkout.WorkoutID }));
        DeleteWorkoutCommand = new Command(DeleteWorkout);

        Exercises = new();
    }



    public void LoadViewData()
    {
        //Update exercise list.
        Exercises.Clear();
        foreach (Exercise e in Session.GetExercisesByWorkout(MyWorkout.WorkoutID))
        {
            Exercises.Add(e);
        }

        //Update the date.
        OnPropertyChanged(nameof(MyWorkout));
    }

    public void SaveData()
    {
        Session.UpdateWorkout(MyWorkout);
    }

    public bool ValidateInputFormat()
    {
        //If any properties of the workout are null or empty (DateTime data type can never be null so a check is unecessary)...
        if (string.IsNullOrWhiteSpace(MyWorkout.WorkoutMuscleGroup))
        {
            ErrorMessage = "Please enter a workout muscle group.";
            return false;
        }
        else
        {
            return true;
        }
    }

    private void DeleteWorkout()
    {
        Session.DeleteWorkout(MyWorkout.WorkoutID);
        //Return to previous page.
        Shell.Current.GoToAsync("..");
    }
}