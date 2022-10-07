using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Workouts : VM_DbAccessor
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
    //test
    public Command TestCommand { get; }



    // Constructor

    public VM_Workouts()
    {
        WorkoutSelectedCommand = new Command<Workout>(WorkoutSelected);
        //GoToPage_CreateWorkout = new Command(()=> Shell.Current.GoToAsync(nameof(Page_CreateWorkout)));

        Workouts = new ObservableCollection<Workout>();

        //test
        TestCommand = new Command(Test);
    }



    // Methods

    public void RefreshWorkouts(string filter = null)
    {
        Workouts.Clear();
        foreach (Workout w in FilterWorkouts(filter))
        {
            Workouts.Add(w);
        }

        WorkoutsHeader = $"Showing {Workouts.Count()} workouts";
        OnPropertyChanged(nameof(WorkoutsHeader));
    }




    //test
    private void Test()
    {
        var workout = new Workout()
        {
            UserID = Session.CurrentUser.UserID,
            Date = "21/10/2004",
            WorkoutMuscleGroup = "Pushh",
            WorkoutComment = ""
        };
        db.Insert(workout);

        RefreshWorkouts();
    }



    //Returns every workout that directly contains a field containing the filter, and everyworkout that contains an exercise that contains a field containing the filter.
    private Workout[] FilterWorkouts(string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            List<Workout> filteredWorkouts = new();
            
            //For each workout associated with this user...
            foreach (Workout w in db.Table<Workout>().Where(w => w.UserID == Session.CurrentUser.UserID).ToArray())
            {
                //If any workout attributes contain the filter...
                if (w.Date.ToLower().Contains(filter.ToLower()) || w.WorkoutMuscleGroup.ToLower().Contains(filter.ToLower()) || w.WorkoutComment.ToLower().Contains(filter.ToLower()))
                    //Add the workout.
                    filteredWorkouts.Add(w);
                //If the workout itself had no relevant data that contained the filter...
                else
                {
                    //...check its exercises.

                    //For each exercise within this workout...
                    foreach (Exercise e in db.Table<Exercise>().Where(e => e.WorkoutID == w.WorkoutID).ToArray())
                    {
                        //Get the exercise type.
                        ExerciseType eT = db.Find<ExerciseType>(e.ExerciseTypeID);

                        //If the exercise type attributes contain the filter...
                        if (eT.ExerciseName.ToLower().Contains(filter.ToLower()) || eT.ExerciseDescription.ToLower().Contains(filter.ToLower()))
                            //Add the workout associated with this exercise.
                            filteredWorkouts.Add(w);
                    }
                }


            }

            return filteredWorkouts.ToArray();
        }
        else
        {
            return db.Table<Workout>().Where(w => w.UserID == Session.CurrentUser.UserID).ToArray();
        }
    }

    private void WorkoutSelected(Workout workout)
    {

    }
}
