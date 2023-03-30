namespace NEA_system.ViewModels;

internal class VM_CreateWorkout : VM_Base, IDatabaseInput
{
    //Properties

    public DateTime Date { get; set; }
    public string WorkoutMuscleGroup { get; set; }
    public string WorkoutComment { get; set; }

    public Command CreateWorkoutCommand { get; }



    //Constructor

    public VM_CreateWorkout()
    {
        CreateWorkoutCommand = new Command(InsertWorkout);

        //Pre-populate entries.
        Date = DateTime.Now;
        //Initialise so that workout can be inserted without null values.
        WorkoutComment = "";
    }



    // Methods

    public bool ValidateInputFormat()
    {
        //If the main input is empty...
        if (string.IsNullOrWhiteSpace(WorkoutMuscleGroup))
        {
            ErrorMessage = "Please enter a workout muscle group.";
            return false;
        }
        else
        {
            return true;
        }
    }

    private void InsertWorkout()
    {
        if (!ValidateInputFormat())
            return;

        //Create and insert the workout.
        var workout = new Workout()
        {
            UserID = Session.CurrentUser.UserID,
            Date = Date.ToShortDateString(),
            WorkoutMuscleGroup = WorkoutMuscleGroup,
            WorkoutComment = WorkoutComment
        };
        Session.InsertWorkout(workout);

        //Pop the create workout page from the stack and push the edit workout page instead.
        Shell.Current.GoToAsync($"../{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}