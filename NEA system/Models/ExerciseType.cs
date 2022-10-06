using SQLite;

namespace NEA_system.Models;

public class ExerciseType
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int ExerciseTypeID { get; set; }
    public int UserID { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseDescription { get; set; }
}