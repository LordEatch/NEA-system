using NEA_system.Models;

namespace NEA_system.ViewModels;

internal class VM_CreateExerciseType
{
    public string ExerciseTypeName { get; set; }
    public string ExerciseTypeDescription { get; set; }

    public Command CreateExerciseTypeCommand { get; }



    public VM_CreateExerciseType()
    {
        CreateExerciseTypeCommand = new Command(InsertExerciseType);
    }



    //Methods

    //FINISH!!!
    private void InsertExerciseType()
    {
        //IF INPUT IS CORRECT


        var eT = new ExerciseType()
        {
            ExerciseTypeName = ExerciseTypeName,
            ExerciseTypeDescription = ExerciseTypeDescription
        };
        Session.DB.Insert(eT);

        //Pop the create exercise type page from the stack.
        Shell.Current.GoToAsync("..");

        System.Diagnostics.Debug.WriteLine($"Exercise type with id: {eT.ExerciseTypeID} inserted.");



        //Subscribe user.
        var sub = new Subscription()
        {
            UserID = Session.CurrentUser.UserID,
            ExerciseTypeID = eT.ExerciseTypeID
        };
        Session.DB.Insert(sub);

        //test
        System.Diagnostics.Debug.WriteLine($"User subscribed to {eT.ExerciseTypeName}.");
    }
}
