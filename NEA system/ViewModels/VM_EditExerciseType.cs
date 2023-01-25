using NEA_system.Models;

namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExerciseType), "ExerciseType")]
internal class VM_EditExerciseType : VM_Base, IRecordEditor
{
    public ExerciseType MyExerciseType { get; set; }



    public void LoadViewData()
    {
        OnPropertyChanged(nameof(MyExerciseType));
    }

    public void SaveData()
    {
        Session.DB.Update(MyExerciseType);
        System.Diagnostics.Debug.WriteLine($"Exercise type with id: {MyExerciseType.ExerciseTypeID} has been updated.");
    }

    public bool ValidateInputFormat()
    {
        if (string.IsNullOrEmpty(MyExerciseType.ExerciseTypeName) || string.IsNullOrEmpty(MyExerciseType.ExerciseTypeDescription))
        {
            ErrorMessage = emptyEntryErrorMessage;
            return false;
        }
        else
        {
            return true;
        }
    }
}