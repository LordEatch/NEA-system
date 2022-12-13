using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_FocusedWorkout : VM_Base, IDataDisplay
{
    public Workout MyWorkout { get; set; }
    public ObservableCollection<Exercise> Exercises { get; set; }

    public Command GoToPage_EditWorkout { get; }


    public VM_FocusedWorkout()
    {
        GoToPage_EditWorkout = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = MyWorkout }));

        Exercises = new();
    }



    public void LoadViewData()
    {
        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            //Pull the name of the exercise's corresponding exercise type.
            var exerciseType = Session.DB.Table<ExerciseType>().Where(eT => eT.ExerciseTypeID == e.ExerciseTypeID).ToArray()[0];
            //Equip the name.
            e.ExerciseName = exerciseType.ExerciseTypeName;

            Exercises.Add(e);
        }

        //Update the date on the view.
        OnPropertyChanged(nameof(MyWorkout));
    }
}