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
        //If the exercise type is already taken...
        else if (Session.ExerciseTypeNameExists(ExerciseTypeName))
        {
            ErrorMessage = "An exercise type with that name already exists.";
            return false;
        }

        return true;
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
        Session.InsertExerciseType(eT);

        //Pop the create exercise type page from the stack.
        Shell.Current.GoToAsync("..");

        System.Diagnostics.Debug.WriteLine($"Exercise type with id: {eT.ExerciseTypeID} inserted.");



        //  Subscribe user

        var sub = new Subscription()
        {
            UserID = Session.CurrentUser.UserID,
            ExerciseTypeID = eT.ExerciseTypeID
        };
        Session.InsertSubscription(sub);

        System.Diagnostics.Debug.WriteLine($"User subscribed to {eT.ExerciseTypeName}.");
    }
}
