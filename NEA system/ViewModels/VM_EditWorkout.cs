namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_FocusedWorkout, IDataDisplay
{
    public VM_EditWorkout()
    {
        Exercises = new();
    }

    public void SaveData()
    {
        Session.DB.Update(MyWorkout);
        System.Diagnostics.Debug.WriteLine($"Workout with id:{MyWorkout.WorkoutID} has been updated.");
    }
}