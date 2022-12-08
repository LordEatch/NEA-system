using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_FocusedWorkout : VM_Base, IDataDisplay
{
    public Workout MyWorkout { get; set; }
    public ObservableCollection<Exercise> Exercises { get; set; }



    public VM_FocusedWorkout()
    {
        Exercises = new();
    }



    public void LoadViewData()
    {
        Exercises.Clear();
        foreach (Exercise e in Session.DB.Table<Exercise>().Where(e => e.WorkoutID == MyWorkout.WorkoutID))
        {
            //Pull the name of the exercise's corresponding exercise type and equip it.
            var exerciseTypes = Session.DB.Table<ExerciseType>().Where(eT => eT.ExerciseTypeID == e.ExerciseTypeID).ToArray();
            e.ExerciseName = exerciseTypes[0].ExerciseTypeName;

            Exercises.Add(e);
        }
    }
}