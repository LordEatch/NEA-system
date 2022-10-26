using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_Base, IDataDisplay
{
    //Properties

    public Workout MyWorkout { get; set; }
    public ObservableCollection<Exercise> Exercises { get; set; }

    // Constructor




    // Methods

    public void LoadViewData()
    {
        //Update exercise list.
        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            Exercises.Add(e);
        }
    }
}