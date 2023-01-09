using SQLite;

namespace NEA_system.Models;

public class Exercise
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int ExerciseID { get; set; }
    [NotNull]
    public int WorkoutID { get; set; }
    [NotNull]
    public int ExerciseTypeID { get; set; }
    [Ignore]
    public string ExerciseName { get; set; }
    {
        get
        {
            var exerciseType = Session.DB.Table<ExerciseType>().Where(eT => eT.ExerciseTypeID == e.ExerciseTypeID).ToArray()[0];
            FINISH
            return Session.DB.Get(1, ExerciseType)
        }
    }
}