using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Workouts : VM_Base, IDataDisplay
{
    // Properties

    //FINISH
    public string Search
    {
        set
        {
            //FINISH make this only go off if Search has not been updated in a second.
            RefreshWorkouts(value);
        }
    }
    public ObservableCollection<Workout> Workouts { get; set; }
    public string WorkoutsHeader { get; set; }

    public Command WorkoutSelectedCommand { get; }
    public Command GoToPage_CreateWorkout { get; }



    // Constructor

    public VM_Workouts()
    {
        WorkoutSelectedCommand = new Command<Workout>(WorkoutSelected);
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

    //FINISH    only make this load the most recent 50 into memory until the user scrolls to the bottom to see more. This is to boost performance since I need to eventaully calculate
    //          exercise and set count per loaded workout and then display it.
    //Returns every workout that directly contains a field containing the filter, and every workout that contains an exercise that contains a field containing the filter.
    private Workout[] FilterWorkouts(string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            List<Workout> filteredWorkouts = new();
            
            //For each workout associated with this user...
            foreach (Workout w in Session.DB.Table<Workout>().Where(w => w.UserID == Session.CurrentUser.UserID))
            {
                //If any workout attributes contain the filter...
                if (w.Date.ToString().Contains(filter.ToLower()) || w.WorkoutMuscleGroup.ToLower().Contains(filter.ToLower()) || w.WorkoutComment.ToLower().Contains(filter.ToLower()))
                    //Add the workout.
                    filteredWorkouts.Add(w);
                //If the workout itself had no relevant data that contained the filter...
                else
                {
                    //...check its exercises.

                    //For each exercise within this workout...
                    foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == w.WorkoutID))
                    {
                        //Get the exercise type.
                        ExerciseType eT = Session.DB.Find<ExerciseType>(e.ExerciseTypeID);

                        //If the exercise type attributes contain the filter...
                        if (eT.ExerciseTypeName.ToLower().Contains(filter.ToLower()) || eT.ExerciseTypeDescription.ToLower().Contains(filter.ToLower()))
                            //Add the workout associated with this exercise.
                            filteredWorkouts.Add(w);
                    }
                }


            }

            return filteredWorkouts.ToArray();
        }
        else
        {
            return Session.DB.Table<Workout>().Where(w => w.UserID == Session.CurrentUser.UserID).ToArray();
        }
    }

    private void WorkoutSelected(Workout workout)
    {
        Shell.Current.GoToAsync($"{nameof(Page_FocusedWorkout)}", new Dictionary<string, object>() { ["Workout"] = workout });
    }
}
