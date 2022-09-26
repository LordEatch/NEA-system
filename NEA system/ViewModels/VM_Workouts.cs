using NEA_system.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(UserID), "UserID")]
internal class VM_Workouts : VM_DbAccessor
{
    // Properties

    public int UserID { get; set; }
    //  filters and sort by here
    public ObservableCollection<Workout> Workouts { get; set; }

    public Command WorkoutSelectedCommand { get; }
    //test
    public Command TestCommand { get; }



    // Constructor

    public VM_Workouts()
    {
        WorkoutSelectedCommand = new Command<Workout>(WorkoutSelected);

        Workouts = new ObservableCollection<Workout>();

        //test
        TestCommand = new Command(Test);
    }



    // Methods

    public void RefreshWorkouts()
    {
        Workouts.Clear();
        foreach (var workout in db.Table<Workout>().Where(w => w.UserID == UserID).ToArray())
        {
            Workouts.Add(workout);
        }
    }

    //test
    private void Test()
    {
        var workout = new Workout()
        {
            UserID = UserID,
            Date = "no-date"
        };
        db.Insert(workout);

        RefreshWorkouts();
    }

    private void WorkoutSelected(Workout workout)
    {

    }
}