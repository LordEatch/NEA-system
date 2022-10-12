using SQLite;
using System.Collections.ObjectModel;

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
    [Ignore]
    //Returns a comma separated string with all exercise types in this workout. Not sure if I will keep?
    public string ExerciseTypesList
    {
        get
        {
            Exercise[] exercises = Session.DB.Table<Exercise>().Where(e => e.WorkoutID == WorkoutID).ToArray();
            string exerciseTypesList = null;

            //For each exercise within this workout...
            for (int i = 0; i < exercises.Length; i++)
            {
                //If on the last element...
                if (i == exercises.Length - 1)
                {
                    //Do not add a comma after the appenditure.
                    exerciseTypesList += Session.DB.Find<ExerciseType>(exercises[i].ExerciseTypeID).ExerciseName;
                }
                else
                {
                    exerciseTypesList = exerciseTypesList + Session.DB.Find<ExerciseType>(exercises[i].ExerciseTypeID).ExerciseName + ", ";
                }

                //Append the string to add the exercise type.
            }

            exerciseTypesList = "fat";

            return exerciseTypesList;
        }
    }
}