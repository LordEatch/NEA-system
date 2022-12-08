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
    public string ExerciseName
    {
        get
        {
            var exerciseTypes = Session.DB.Table<ExerciseType>().Where(eT => eT.ExerciseTypeID == ExerciseTypeID).ToArray();
            if (exerciseTypes == null)
            {
                return "No corresponding exercise type found.";
            }
            else
            {
                return exerciseTypes[0].ExerciseTypeName;
            }
        }
    }

    public static IEnumerable<string> QueryVals()
    {
        return Session.DB.Query<string>("SELECT \"ExerciseTypeName\" FROM ExerciseType WHERE ExerciseID = ?", this.ExerciseID);
    }