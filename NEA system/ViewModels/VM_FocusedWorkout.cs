namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_FocusedWorkout : VM_Base, IDataDisplay
{
    public Workout MyWorkout { get; set; }


    public void LoadViewData()
    {

    }
}