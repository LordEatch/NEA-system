using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

//THIS QUERY PROPERTY ISNT PASSING!!!!
[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_Base, IDataDisplay
{
    public Workout MyWorkout { get; set; }
    public ObservableCollection<Exercise> Exercises { get; set; }



    public VM_EditWorkout()
    {
        Exercises = new();
    }



    public void LoadViewData()
    {
        //test
        System.Diagnostics.Debug.WriteLine("edit workout myworkout id: " + MyWorkout.WorkoutID);

        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            Exercises.Add(e);
        }
    }
}