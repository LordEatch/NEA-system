﻿using SQLite;

namespace NEA_system.Models;

public class ResistanceSet
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int SetID { get; set; }
    [NotNull]
    public int ExerciseID { get; set; }
    public double Mass { get; set; }
    public int StrictReps { get; set; }
    public int CheatedReps { get; set; }
    [NotNull]
    public string SetComment { get; set; }
}