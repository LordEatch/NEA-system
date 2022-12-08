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
    //FINISH Not sure if this works??
    [Ignore]
    public string ExerciseName { get; set; }
}