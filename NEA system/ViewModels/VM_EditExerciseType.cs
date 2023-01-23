namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyExerciseType), "ExerciseType")]
internal class VM_EditExerciseType : VM_Base, IDataDisplay
{
    public ExerciseType MyExerciseType { get; set; }



    public VM_EditExerciseType()
    {

    }



    public void LoadViewData()
    {
        OnPropertyChanged(nameof(MyExerciseType));
    }

    public void SaveData()
    {
        //insert the exercise type
        //subscribe the user to the exercise type
    }
}