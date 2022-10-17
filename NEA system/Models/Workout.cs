using SQLite;

namespace NEA_system.Models;

public class Workout
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int WorkoutID { get; set; }
    [NotNull]
    public int UserID { get; set; }
    [NotNull]
    //FINISH not sure if this will break?
    public DateTime Date { get; set; }
    [NotNull]
    public string WorkoutMuscleGroup { get; set; }
    [NotNull]
    public string WorkoutComment { get; set; }
}