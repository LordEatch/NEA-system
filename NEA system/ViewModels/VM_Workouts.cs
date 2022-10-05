using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_Workouts : VM_DbAccessor
{
    // Properties

    public User MyUser { get; set; }
    
    public string Search
    {
        get;
        set
        {
            //Search = value;
		    //if (Seach not changed in 1 second)
		    //{
		    //	RefreshWorkouts(Search);
		    //}
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
            Date = "no-date",
            WorkoutMuscleGroup = "Pushh"
        };
        db.Insert(workout);

        RefreshWorkouts();
    }
    
    
    
    

    private void RefreshWorkouts(string filter)
    {
        Workouts.Clear();
        //foreach (workout in FilterWorkouts(filter))
		//{
		//  Workouts.Add(workout);
		//}


        
        //OLD CODE
        //foreach (var workout in db.Table<Workout>().Where(w => w.UserID == MyUser.UserID).ToArray())
        //{
        //    Workouts.Add(workout);
        //}
    }

    private Workout[] FilterWorkouts(string filter)
    {
        //if (filter != NullOrWhiteSpace)
	    //{
		//    Return anything relevant in the database that is equal to the filter:

		//    Workout properties,
		//    Exercise properties (return the workout associated with those sets)
		//    Set properties (return the workout associated with those sets)
	    //}
        //else
        //{
        //  return all workouts;
        //}
    }

    private void WorkoutSelected(Workout workout)
    {

    }
}
