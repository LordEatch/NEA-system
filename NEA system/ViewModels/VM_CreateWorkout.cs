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

        //test
        System.Diagnostics.Debug.WriteLine($"Workout created with user id: {workout.UserID}, workout id: '{workout.WorkoutID}', date: '{workout.Date}', muscle group: '{workout.WorkoutMuscleGroup}' and comment: '{workout.WorkoutComment}'");





        //NOT WORKING this is passing a null workout to VM_EditWorkout (most likely error on the edit workout vm's end).

        //Pop the create workout page from the stack and push the focused workout page instead.
        //Shell.Current.GoToAsync($"../{nameof(Page_FocusedWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
        //Proceed to the edit workout page.
        Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}