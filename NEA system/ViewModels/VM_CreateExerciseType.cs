namespace NEA_system.ViewModels;

internal class VM_CreateExerciseType : VM_Base, IDatabaseInput
{
    public string ExerciseTypeName { get; set; }
    public string ExerciseTypeDescription { get; set; }

    public Command CreateExerciseTypeCommand { get; }



    public VM_CreateExerciseType()
    {
        CreateExerciseTypeCommand = new Command(InsertExerciseType);
    }



    //  Methods

    public bool ValidateInputFormat()
    {
        //If an entry is empty...
        if (string.IsNullOrWhiteSpace(ExerciseTypeName) || string.IsNullOrWhiteSpace(ExerciseTypeDescription))
        {
            ErrorMessage = emptyEntryErrorMessage;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void InsertExerciseType()
    {
        //  Create exercise type

        //Cancel if the input is bogus.
        if (!ValidateInputFormat())
            return;

        var eT = new ExerciseType()
        {
            ExerciseTypeName = ExerciseTypeName,
            ExerciseTypeDescription = ExerciseTypeDescription
        };
        Session.DB.Insert(eT);

        //Pop the create exercise type page from the stack.
        Shell.Current.GoToAsync("..");

        System.Diagnostics.Debug.WriteLine($"Exercise type with id: {eT.ExerciseTypeID} inserted.");



        //  Subscribe user

        var sub = new Subscription()
        {
            UserID = Session.CurrentUser.UserID,
            ExerciseTypeID = eT.ExerciseTypeID
        };
        Session.DB.Insert(sub);

        System.Diagnostics.Debug.WriteLine($"User subscribed to {eT.ExerciseTypeName}.");
    }
}
