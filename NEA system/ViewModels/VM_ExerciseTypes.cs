using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;
internal class VM_ExerciseTypes : VM_Base, IDataDisplay
{
    public ObservableCollection<ExerciseType> ExerciseTypes { get; set; }

    public VM_ExerciseTypes()
    {
        ExerciseTypes = new();
    }

    public void LoadViewData()
    {
        ExerciseTypes.Clear();
        foreach (ExerciseType eT in Session.DB.Table<ExerciseType>())
        {
            ExerciseTypes.Add(eT);
        }
    }
}