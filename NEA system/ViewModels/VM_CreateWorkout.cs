﻿namespace NEA_system.ViewModels;

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
    }



    // Methods

    public bool ValidateInputFormat()
    {
        //If either input is empty...
        if (string.IsNullOrEmpty(WorkoutMuscleGroup) || string.IsNullOrEmpty(WorkoutComment))
        {
            ErrorMessage = "Please enter information into each box.";
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
        //



        //Pop the create workout page from the stack and push the edit workout page instead.
        Shell.Current.GoToAsync($"../{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}