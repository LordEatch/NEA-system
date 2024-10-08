﻿using SQLite;

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

    //Ignored by SQLite. No field in the exercise table called 'ExerciseName' will be created. This is only used by 'Page_EditWorkout' to display relevant exercise names.
    [Ignore]
    public string ExerciseName
    {
        get
        {
            return Session.GetExerciseType(ExerciseTypeID).ExerciseTypeName;
        }
    }
}