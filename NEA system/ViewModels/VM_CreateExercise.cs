namespace NEA_system.ViewModels;

[QueryProperty(nameof(WorkoutID), "WorkoutID")]
internal class VM_CreateExercise : VM_Base, IDatabaseInput
{
    public int WorkoutID { get; set; }
    public List<ExerciseType> ExerciseTypes { get; set; }
    public ExerciseType SelectedExerciseType { get; set; }

    public Command CreateExerciseCommand { get; }



    public VM_CreateExercise()
    {
        CreateExerciseCommand = new Command(InsertExercise);

        //Populate the picker.
        ExerciseTypes = Session.GetSubscribedExerciseTypes().ToList();
    }



    public bool ValidateInputFormat()
    {
        if (SelectedExerciseType == null)
        {
            ErrorMessage = "Please select an exercise.";
            return false;
        }

        return true;
    }

    public void InsertExercise()
    {
        if (!ValidateInputFormat())
            return;

        //Create and insert the exercise.
        var exercise = new Exercise()
        {
            WorkoutID = WorkoutID,
            ExerciseTypeID = SelectedExerciseType.ExerciseTypeID
        };
        Session.InsertExercise(exercise);

        //test
        System.Diagnostics.Debug.WriteLine($"Exercise with id:{exercise.ExerciseID} and exercise type id:{exercise.ExerciseTypeID} inserted into workout id:{exercise.WorkoutID}.");

        //Pop the create exercise page from the stack and push the edit exercise page instead.
        Shell.Current.GoToAsync($"../{nameof(Page_EditExercise)}", new Dictionary<string, object>() { ["Exercise"] = exercise });
    }
}
