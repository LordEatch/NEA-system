using SQLite;

namespace NEA_system.Models;

public class ExerciseType
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int ExerciseTypeID { get; set; }
    public int UserID { get; set; }
    [NotNull]
    public string ExerciseName { get; set; }
    public string ExerciseDescription { get; set; }



    //Methods

    public ExerciseType PullExerciseType()
    {
        //FINISH
        return new ExerciseType();
    }
}