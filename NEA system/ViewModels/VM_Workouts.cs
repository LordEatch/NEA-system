using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Workouts : VM_Base, IDataDisplay
{
    // Properties

    public string Search
    {
        get { return null; }
        //Called every time the user updates the search bar (types a character).
        set
        {
            RefreshWorkouts(value.ToLower());
        }
    }
    public ObservableCollection<Workout> Workouts { get; set; }
    public Workout SelectedWorkout
    {
        get { return null; }
        set
        {
            //If a workout was selected...
            if (value != null)
                //Go to its page.
                Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = value });
        }
    }
    public string WorkoutsHeader { get; set; }

    public Command GoToPage_CreateWorkout { get; }



    // Constructor

    public VM_Workouts()
    {
        GoToPage_CreateWorkout = new Command(() => Shell.Current.GoToAsync(nameof(Page_CreateWorkout)));

        Workouts = new();
    }



    // Methods

    public void LoadViewData()
    {
        RefreshWorkouts();
    }

    private void RefreshWorkouts(string filter = null)
    {
        Workouts.Clear();
        foreach (Workout w in FilterWorkouts(filter))
        {
            Workouts.Add(w);
        }



        WorkoutsHeader = $"Showing {Workouts.Count()} workouts";
        OnPropertyChanged(nameof(WorkoutsHeader));
    }

    //Returns every workout that directly contains a field containing the filter, and every workout that contains an exercise that contains a field containing the filter.
    private static Workout[] FilterWorkouts(string filter)
    {
        //
        if (!string.IsNullOrWhiteSpace(filter))
        {
            List<Workout> filteredWorkouts = new();

            //FINISH need to add ability to search for date. (Ticks cannot be searched for).
            string query = @$"
                SELECT DISTINCT Workout.WorkoutID, Workout.UserID, Workout.Date, Workout.WorkoutMuscleGroup, Workout.WorkoutComment
                FROM Workout
                INNER JOIN Exercise ON Exercise.WorkoutID = Workout.WorkoutID
                INNER JOIN ExerciseType ON Exercise.ExerciseTypeID = ExerciseType.ExerciseTypeID
                WHERE Workout.UserID = '{Session.CurrentUser.UserID}'
                AND (ExerciseTypeName LIKE '%{filter}%'
                OR ExerciseTypeDescription LIKE '%{filter}%'
                OR Workout.WorkoutMuscleGroup LIKE '%{filter}%'
                OR WorkoutComment LIKE '%{filter}%')";

            foreach (Workout w in Session.DB.Query<Workout>(query))
            {
                filteredWorkouts.Add(w);
            }

            return filteredWorkouts.ToArray();
        }
        else
        {
            return Session.DB.Table<Workout>().Where(w => w.UserID == Session.CurrentUser.UserID).ToArray();
        }
    }
}