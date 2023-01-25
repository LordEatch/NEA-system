namespace NEA_system.ViewModels;

internal class VM_CreateWorkout : VM_Base
{
    //Properties

    public DateTime Date { get; set; }
    public string WorkoutMuscleGroup { get; set; }
    public string WorkoutComment { get; set; }

    public Command CreateWorkoutCommand { get; }



    //Constructor

    public VM_CreateWorkout()
    {
        CreateWorkoutCommand = new Command(CreateWorkout);

        //Pre-populate entries.
        Date = DateTime.Now;
    }



    // Methods

    private void CreateWorkout()
    {
        if (ValidateInput())
            InsertWorkout();
    }

    private bool ValidateInput()
    {
        //If both inputs are full...
        if (!(string.IsNullOrEmpty(WorkoutMuscleGroup) || string.IsNullOrEmpty(WorkoutComment)))
            return true;

        ErrorMessage = "Please enter information into each box.";
        return false;
    }

    private void InsertWorkout()
    {
        var workout = new Workout()
        {
            UserID = Session.CurrentUser.UserID,
            Date = Date,
            WorkoutMuscleGroup = WorkoutMuscleGroup,
            WorkoutComment = WorkoutComment
        };
        Session.DB.Insert(workout);

        //test ADD TEMPORARY EXERCISES
        var e = new Exercise()
        {
            WorkoutID = workout.WorkoutID,
            ExerciseTypeID = 1
        };
        Session.DB.Insert(e);
        Session.DB.Insert(e);
        Session.DB.Insert(e);

        //Pop the create workout page from the stack and push the edit workout page instead.
        Shell.Current.GoToAsync($"../{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}