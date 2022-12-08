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
        foreach (ExerciseType eT in Session.DB.Table<ExerciseType>())
        {
            //FINISH need to code the collectionview n stuff.
        }
    }
}