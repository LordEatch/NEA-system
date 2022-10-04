using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_Workouts : VM_DbAccessor
{
    // Properties

    public User MyUser { get; set; }


    //  filters and sort_by here

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

    public void InitialiseVM()
    {
        RefreshWorkouts();
        //I would have this as a getter under the property but MAUI bug means that query props
        //do not pass until some time after the page appears (need UserID to get number of workouts).
        //This method is delayed later to fix.

        NumberOfWorkouts = db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).Count().ToString();
        OnPropertyChanged(nameof(NumberOfWorkouts));
    }

    private void RefreshWorkouts()
    {
        Workouts.Clear();
        foreach (var workout in db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).ToArray())
        {
            Workouts.Add(workout);
        }
    }

    //test
    private void Test()
    {
        var workout = new Workout()
        {
            UserID = MyUser.UserID,
            Date = "no-date",
            WorkoutMuscleGroup = "Pushh"
        };
        db.Insert(workout);

        RefreshWorkouts();
    }

    private void WorkoutSelected(Workout workout)
    {

    }
}