using NEA_system.Models;
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
            //test
            System.Diagnostics.Debug.WriteLine(CalculateOneRepMax());
            return $"Your predicted 1RM: {CalculateOneRepMax()}";
        }
    }


    public Command AddSetCommand { get; set; }



    //  Constructor

    public VM_EditExercise()
    {
        AddSetCommand = new Command(AddSet);
        ResistanceSets = new();
    } 



    //  Methods

    public void LoadViewData()
    {
        ResistanceSets.Clear();

        foreach (ResistanceSet set in Session.DB.Table<ResistanceSet>().Where(rS => rS.ExerciseID == MyExercise.ExerciseID))
        {
            ResistanceSets.Add(set);
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
        return false;
    }

    private void AddSet()
    {
        ResistanceSets.Add(new ResistanceSet());
        System.Diagnostics.Debug.WriteLine($"Resistance set added.");
        OnPropertyChanged(OneRepMaxLabel);
    }

    //FINISH
    private int CalculateOneRepMax()
    {
        //If there are no sets performed yet...
        if (ResistanceSets.Count() == 0)
        {
            //FINISH
            //...Calculate a 1RM value based on the last set performed of this exercise.
            return -1;
        }
        else
        {
            //...Calculate an average 1RM value based on the sets performed.
            double oneRepMaxValue = 0;

            foreach (ResistanceSet rS in ResistanceSets)
            {
                //Epley equation.
                oneRepMaxValue += rS.Mass * (1 + (rS.StrictReps / 30));
            }

            //Calculate mean.
            return (int)Math.Round(oneRepMaxValue / ResistanceSets.Count());
        }
    }
}