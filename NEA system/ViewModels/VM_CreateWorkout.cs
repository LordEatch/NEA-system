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
        CreateWorkoutCommand = new Command(InsertWorkout);

        //Pre-populate entries.
        Date = DateTime.Now;
        WorkoutMuscleGroup = "";
        WorkoutComment = "";
    }



    // Methods

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

        //Pop the create workout page from the stack and push the focused workout page instead.
        Shell.Current.GoToAsync($"../{nameof(Page_FocusedWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
        //Proceed to the edit workout page.
        Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}