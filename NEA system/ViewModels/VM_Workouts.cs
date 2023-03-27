using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Workouts : VM_Base, IDatabaseOutput
{
    // Properties

    //Private field used for getting in 'LoadViewData'.
    private string search;
    public string Search
    {
        get { return search; }
        //Called every time the user updates the search bar (types a character).
        set
        {
            search = value;
            if (!String.IsNullOrWhiteSpace(search))
            {
                //Refresh workouts and pass Search as the parameter.
                RefreshWorkouts(search.ToLower());
            }
            else
            {
                //Refresh workouts with default search (all user workouts).
                RefreshWorkouts(null);
            }
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
        RefreshWorkouts(Search);
    }

    
    private void RefreshWorkouts(string filter)
    {
        //  Query the database for workouts

        Workouts.Clear();

        string query = @$"
                SELECT DISTINCT Workout.WorkoutID, UserID, Date, WorkoutMuscleGroup, WorkoutComment
                FROM Exercise, ExerciseType, Workout
				WHERE Workout.UserID = '{Session.CurrentUser.UserID}'
				AND (ExerciseTypeName LIKE '%{filter}%'
                OR Workout.Date LIKE '%{filter}%'
                OR Workout.WorkoutMuscleGroup LIKE '%{filter}%'
                OR WorkoutComment LIKE '%{filter}%')
                ORDER BY Date DESC";

        int workoutCount = 0;
        //Populate the observable collection.
        foreach (Workout w in Session.DB.Query<Workout>(query))
        {
            Workouts.Add(w);
            workoutCount++;
        }
        


        //  Update the header

        WorkoutsHeader = $"Showing {workoutCount} workouts";
        OnPropertyChanged(nameof(WorkoutsHeader));
    }    
}