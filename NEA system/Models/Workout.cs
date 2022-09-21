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
    public string Date { get; set; }
    public string WorkoutMuscleGroup { get; set; }
    public string WorkoutComment { get; set; }



    //Static methods

    //Creates and inserts Workout and returns the id.
    public static int CreateWorkout(SQLiteConnection db, int userID, string date, string workoutMuscleGroup, string workoutComment)
    {
        if (date == null)
        {
            date = "no-date-given";
        }

        var workout = new Workout()
        {
            UserID = userID,
            Date = date,
            WorkoutMuscleGroup = workoutMuscleGroup,
            WorkoutComment = workoutComment
        };
        db.Insert(workout);

        return workout.WorkoutID;
    }

    public static Workout[] PullWorkouts(SQLiteConnection db, int userID)
    {
        var query = db.Table<Workout>().Where(v => v.UserID.Equals(userID));
        var records = query.ToArray();
        return records;
    }

    //Returns true if the operation was successful.
    //public static bool ReplaceWorkout(SQLiteConnection db, int oldWorkoutID, Workout newWorkout)
    //{
    //    //If the old workout exists...
    //    if (db.Find<Workout>(oldWorkoutID) != null)
    //    {
    //        //Delete the old workout from the workout table.
    //        db.Delete(oldWorkoutID);

    //        //Insert a new workout.
    //        db.Insert(CreateWorkout(oldWorkoutID, newWorkout.UserID, newWorkout.Date, newWorkout.WorkoutMuscleGroup, newWorkout.WorkoutComment));
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}