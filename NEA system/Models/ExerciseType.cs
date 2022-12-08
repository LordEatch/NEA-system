using SQLite;

namespace NEA_system.Models;

public class ExerciseType
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int ExerciseTypeID { get; set; }
    [NotNull]
    public string ExerciseTypeName { get; set; }
    [NotNull]
    public string ExerciseTypeDescription { get; set; }
}