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



        //Methods

        public static Exercise[] PullExercises(SQLiteConnection db, int workoutID)
        {
            return db.Table<Exercise>().Where(exercise => exercise.WorkoutID.Equals(workoutID)).ToArray();
        }
    }