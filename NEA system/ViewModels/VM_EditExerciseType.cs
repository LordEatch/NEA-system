﻿namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExerciseType), "ExerciseType")]
internal class VM_EditExerciseType : VM_Base, IRecordEditor
{
    public ExerciseType MyExerciseType { get; set; }

    public Command DeleteExerciseTypeCommand { get; set; }


    
    public VM_EditExerciseType()
    {
        DeleteExerciseTypeCommand = new Command(DeleteExerciseType);
    }



    //Updates entries and title (both depend on 'MyExerciseType').
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
        if (string.IsNullOrWhiteSpace(MyExerciseType.ExerciseTypeName) || string.IsNullOrWhiteSpace(MyExerciseType.ExerciseTypeDescription))
        {
            ErrorMessage = emptyEntryErrorMessage;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DeleteExerciseType()
    {
        //Delete record.
        Session.DB.Delete<ExerciseType>(MyExerciseType.ExerciseTypeID);
        //Return to previous page.
        Shell.Current.GoToAsync("..");
    }
}