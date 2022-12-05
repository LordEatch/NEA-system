using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty (nameof(MyWorkout), "Workout")]
internal class VM_EditWorkout : VM_FocusedWorkout, IDataDisplay
{
    public VM_EditWorkout()
    {
        Exercises = new();
    }
}