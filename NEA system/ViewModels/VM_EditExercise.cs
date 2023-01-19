using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExercise), "Exercise")]
internal class VM_EditExercise : VM_Base, IDataDisplay
{
    public Exercise MyExercise { get; set; }
    public ObservableCollection<ResistanceSet> ResistanceSets { get; set; }



    public VM_EditExercise()
    {
        ResistanceSets = new ObservableCollection<ResistanceSet>();
    } 



    public void LoadViewData()
    {
        ResistanceSets.Clear();
        ResistanceSets.Add(new ResistanceSet());
    }

    public void SaveData()
    {
        Session.DB.Update(MyExercise);
        System.Diagnostics.Debug.WriteLine($"Exercise with id:{MyExercise.ExerciseID} has been updated.");
    }
}