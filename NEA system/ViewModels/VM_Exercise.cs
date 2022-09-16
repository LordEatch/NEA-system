using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels
{
    [QueryProperty(nameof(ExerciseID), "ExerciseID")]
    internal class VM_Exercise
    {
        public int ExerciseID { get; set; }

        private readonly Set[] sets;
        public ObservableCollection<Set> Sets { get; set; }



        //Constructor

        public VM_Exercise()
        {
            //test.
            sets = new Set[] { new Set(), new Set()};
            UpdateSets();
        }

        private void UpdateSets()
        {
            Sets = new ObservableCollection<Set>(sets);
        }
    }
}
