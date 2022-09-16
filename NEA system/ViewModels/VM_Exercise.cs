using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(ExerciseID), "ExerciseID")]
internal class VM_Exercise : VM_Base
{
    public int ExerciseID { get; set; }



    #region sets
    private ObservableCollection<Set> sets;
    public ObservableCollection<Set> Sets
    {
        get { return sets; }
        set
        {
            sets = value;
            OnPropertyChanged(nameof(Sets));
        }
    }
    #endregion



    //Constructor

    public VM_Exercise()
    {
        //test.
        sets = new ObservableCollection<Set> { new Set(), new Set() };
    }

    public void UpdateSets()
    {
        Sets = new ObservableCollection<Set>(sets);
    }
}