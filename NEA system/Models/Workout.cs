using SQLite;

namespace NEA_system.Models;

public class Workout
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int WorkoutID { get; set; }
    [NotNull]
    public int UserID { get; set; }
    public string Date { get; set; }
    public string WorkoutMuscleGroup { get; set; }
    public string WorkoutComment { get; set; }
}