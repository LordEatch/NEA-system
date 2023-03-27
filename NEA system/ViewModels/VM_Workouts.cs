using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Workouts : VM_Base, IDatabaseOutput
{
    // Properties

    private string search;
    public string Search
    {
        get { return search; }
        //Called every time the user updates the search bar (types a character).
        set
        {
            search = value;
            if (!String.IsNullOrWhiteSpace(search))
            {
                //Refresh workouts and pass Search as the parameter.
                RefreshWorkouts(search.ToLower());
            }
            else
            {
                //Refresh workouts with default search (all user workouts). If lots of white space is searched, no workouts will show up.
                RefreshWorkouts(null);
            }
        }
    }
    public ObservableCollection<Workout> Workouts { get; set; }
    public Workout SelectedWorkout
    {
        get { return null; }
        set
        {
            //If a workout was selected...
            if (value != null)
                //Go to its page.
                Shell.Current.GoToAsync($"{nameof(Page_EditWorkout)}", new Dictionary<string, object>() { ["Workout"] = value });
        }
    }
    public string WorkoutsHeader { get; set; }

    public Command GoToPage_CreateWorkout { get; }



    // Constructor

    public VM_Workouts()
    {
        GoToPage_CreateWorkout = new Command(() => Shell.Current.GoToAsync(nameof(Page_CreateWorkout)));

        Workouts = new();
    }



    // Methods

    public void LoadViewData()
    {
        RefreshWorkouts(Search);
    }

    private void RefreshWorkouts(string filter)
    {
        int workoutCount = 0;

        //Populate the observable collection.
        Workouts.Clear();
        foreach (Workout w in Session.GetWorkouts(filter))
        {
            Workouts.Add(w);
            workoutCount++;
        }
        
        //Update the header.
        WorkoutsHeader = $"Showing {workoutCount} workouts";
        OnPropertyChanged(nameof(WorkoutsHeader));
    }    
}