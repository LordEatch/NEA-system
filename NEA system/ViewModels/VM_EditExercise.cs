using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExercise), "Exercise")]
internal class VM_EditExercise : VM_Base, IRecordEditor
{
    //  Properties

    public Exercise MyExercise { get; set; }
    public ObservableCollection<ResistanceSet> ResistanceSets { get; set; }
    public string OneRepMaxLabel { get; set; }


    public Command AddSetCommand { get; set; }
    public Command DeleteSetCommand { get; set; }
    public Command FinishExerciseCommand { get; set; }
    public Command DeleteExerciseCommand { get; set; }



    //  Constructor

    public VM_EditExercise()
    {
        AddSetCommand = new Command(AddSet);
        DeleteSetCommand = new Command<ResistanceSet>(DeleteSet);
        FinishExerciseCommand = new Command(FinishExercise);
        DeleteExerciseCommand = new Command(DeleteExercise);

        ResistanceSets = new();
    } 



    //  Methods

    public void LoadViewData()
    {
        //If the list view is empty (and the page has just loaded or there are no sets in the db)... (this is so that sets arent deleted when tabbing out)
        if (ResistanceSets.Count() == 0)
        {
            //Get relevant sets.
            foreach (ResistanceSet set in Session.GetResistanceSetsByExercise(MyExercise.ExerciseID))
            {
                ResistanceSets.Add(set);
            }
        }

        OneRepMaxLabel = $"1RM: {CalculateOneRepMax()}";
        OnPropertyChanged(nameof(OneRepMaxLabel));
    }

    public void SaveData()
    {
        Session.UpdateExercise(MyExercise);
    }

    //FINISH
    public bool ValidateInputFormat()
    {
        //FINISH
        //Need to check every set.

        //Comment is already initialised and does not need to be checked for null.

        return true;
    }



    private void AddSet()
    {
        var set = new ResistanceSet
        {
            ExerciseID = MyExercise.ExerciseID,
            Mass = 0,
            StrictReps = 0,
            CheatedReps = 0,
            SetComment = ""
        };

        //Get the most recent set (null set if there are no other sets).
        var lastSet = Session.GetResistanceSetsByExerciseType(MyExercise.ExerciseTypeID).FirstOrDefault();
        //If a last set exists...
        if (lastSet != null)
            //Get last mass used.
            set.Mass = lastSet.Mass;

        ResistanceSets.Add(set);
    }

    private void DeleteSet(ResistanceSet set)
    {
        Session.DeleteResistanceSet(set.SetID);
        ResistanceSets.Remove(set);
    }

    private void FinishExercise()
    {
        if (!ValidateInputFormat())
            return;

        //Write every set to the database.
        foreach (ResistanceSet set in ResistanceSets)
        {
            //If the set already exists in the database...
            if (Session.GetResistanceSet(set.SetID) != null)
            {
                //Update the existing set.
                Session.UpdateResistanceSet(set);
            }
            else
            {
                //Insert a new resistance set.
                Session.InsertResistanceSet(set);
            }
        }

        //Return to previous page.
        Shell.Current.GoToAsync("..");
    }

    private void DeleteExercise()
    {
        Session.DeleteExercise(MyExercise.ExerciseID);
        //Return to previous page.
        Shell.Current.GoToAsync("..");
    }

    //Use the Epley equation to estimate a one rep max based on the last performed set.
    private int CalculateOneRepMax()
    {
        var set = Session.GetResistanceSetsByExerciseType(MyExercise.ExerciseTypeID).FirstOrDefault();

        if (set != null)
        {
            //Initialise as doubles to allow division of integers into fractions.
            double mass = set.Mass;
            double reps = set.StrictReps;

            //Epley equation.
            return (int)Math.Round(mass * (1 + (reps / 30)));
        }
        else
        {
            return -1;
        }
    }
}