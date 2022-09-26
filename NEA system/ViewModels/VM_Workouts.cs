using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(UserID), "UserID")]
internal class VM_Workouts : VM_DbAccessor
{
    // Properties

    public int UserID { get; set; }


    //  filters and sort by here

    public string NumberOfWorkouts { get; set; }
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

    //FINISH
    public void InitialiseVM()
    {
        RefreshWorkouts();
        //FINISH /// PLEASE FIX THIS SHIT. updates the property and calls on property changed but property does not change on page and string remains empty.
        //I would have this as a getter under the property but MAUI bug means that query props
        //do not pass until some time after the page appears (need UserID to get number of workouts).
        //This method is delayed later to fix.
        System.Diagnostics.Debug.WriteLine("uid: " + UserID);
        int x = db.Table<Workout>().Where(w => w.UserID == UserID).Count();
        NumberOfWorkouts = x.ToString();
        OnPropertyChanged(nameof(NumberOfWorkouts));
        System.Diagnostics.Debug.WriteLine("noW: " + NumberOfWorkouts);
    }
    private void RefreshWorkouts()
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