using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(ExerciseID), "ExerciseID")]
internal class VM_Exercise : VM_Base
{
    public int ExerciseID { get; set; }

    #region sets
    private ResistanceSet[] sets;
    public ObservableCollection<ResistanceSet> Sets { get; set; }
    #endregion



    // Constructor

    public VM_Exercise()
    {
        //test.
        Sets = new ObservableCollection<ResistanceSet>(new ResistanceSet[] { new ResistanceSet() });
    }
}