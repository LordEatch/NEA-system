using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

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
        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            Exercises.Add(e);
        }
    }
}