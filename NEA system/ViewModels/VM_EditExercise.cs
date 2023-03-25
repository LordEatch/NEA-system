using System.Diagnostics;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExercise), "Exercise")]
internal class VM_EditExercise : VM_Base, IRecordEditor
{
    //  Properties

    public Exercise MyExercise { get; set; }
    public ObservableCollection<ResistanceSet> ResistanceSets { get; set; }
    public string OneRepMaxLabel
    {
        get
        {
            return $"Your predicted 1RM: {CalculateOneRepMax()}";
        }
    }


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
        ResistanceSets.Clear();

        //Get relevant sets.
        foreach (ResistanceSet set in Session.DB.Table<ResistanceSet>().Where(rS => rS.ExerciseID == MyExercise.ExerciseID))
        {
            ResistanceSets.Add(set);
            Debug.WriteLine($"Set with id:{set.SetID}, exerciseID:{set.ExerciseID} found.");
        }
    }

    public void SaveData()
    {
        Session.DB.Update(MyExercise);
        System.Diagnostics.Debug.WriteLine($"Exercise with id: {MyExercise.ExerciseID} has been updated.");
    }

    //FINISH
    public bool ValidateInputFormat()
    {
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

        ResistanceSets.Add(set);
    }

    private void DeleteSet(ResistanceSet set)
    {
        Session.DB.Delete<ResistanceSet>(set.SetID);
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
            if (Session.DB.Table<ResistanceSet>().Where(rS => rS.SetID == set.SetID).ToArray().Length == 1)
            {
                Session.DB.Update(set);
                Debug.WriteLine($"Set with id:{set.SetID} updated.");
            }
            else
            {
                Session.DB.Insert(set);
                Debug.WriteLine($"Set with id:{set.SetID}, exerciseID:{set.ExerciseID} inserted.");
            }
        }

        //Return to previous page.
        Shell.Current.GoToAsync("..");
    }

    //NOTE this does not delete all relevant sets.
    private void DeleteExercise()
    {
        Session.DB.Delete<Exercise>(MyExercise.ExerciseID);
        Shell.Current.GoToAsync("..");
    }

    //FINISH
    private int CalculateOneRepMax()
    {
        //FINISH
        //Get last rS!

        ResistanceSet rS = new();

        return (int)Math.Round(rS.Mass * (1 + (rS.StrictReps / 30)));
    }
}