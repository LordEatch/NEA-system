using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_Workouts : VM_DbAccessor
{
    // Properties

    public User MyUser { get; set; }
    //FINISH
    public string Search
    {
        set
        {
            //FINISH make this only go off if Search has not been updated in a second.
            RefreshWorkouts(value);
        }
    }
    public ObservableCollection<Workout> Workouts { get; set; }
    public string NumberOfWorkouts { get; set; }



    public Command WorkoutSelectedCommand { get; }
    public Command GoToPage_CreateWorkout { get; }
    //test
    public Command TestCommand { get; }



    // Constructor

    public VM_Workouts()
    {
        WorkoutSelectedCommand = new Command<Workout>(WorkoutSelected);
        //GoToPage_CreateWorkout = new Command(()=> Shell.Current.GoToAsync(nameof(Page_CreateWorkout)));

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
        //This method is delayed in Page.OnAppearing() to fix.
        NumberOfWorkouts = db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).Count().ToString();
        OnPropertyChanged(nameof(NumberOfWorkouts));
    }
    
    
    
    //test
    private void Test()
    {
        var workout = new Workout()
        {
            UserID = MyUser.UserID,
            Date = "21/10/2004",
            WorkoutMuscleGroup = "Pushh"
        };
        db.Insert(workout);

        RefreshWorkouts();
    }
    
    
    
    

    private void RefreshWorkouts(string filter = null)
    {
        Workouts.Clear();
        foreach (Workout w in FilterWorkouts(filter))
        {
            Workouts.Add(w);
        }
    }

    //FINISH
    private Workout[] FilterWorkouts(string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            //FINISH Return anything relevant in the database that is equal to the filter:

            //Workout properties,
            //Exercise properties(return the workout associated with those sets)
            //Set properties(return the workout associated with those sets)

            //FIX ordering by date but as an integer so its not really ordered properly.
            return db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).OrderBy(w => w.Date).ToArray();
        }
        else
        {
            return db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).OrderBy(w => w.Date).ToArray();
        }
    }

    private void WorkoutSelected(Workout workout)
    {

    }
}
