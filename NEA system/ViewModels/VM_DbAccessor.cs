using SQLite;
using System.Diagnostics;

namespace NEA_system.ViewModels;

internal abstract class VM_DbAccessor : VM_Base
{
    //FINISH android app not working because this isnt async?
    protected SQLiteConnection db;

    protected VM_DbAccessor(string dbName = "GymDatabase.db")
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);

        //If the database file does not already exist...
        if (!File.Exists(dbPath))
        {
            Debug.WriteLine($"Creating new database at {dbPath}...");
            db = new SQLiteConnection(dbPath);
            if (db == null) Debug.WriteLine("Database could not connect.");
            Debug.WriteLine("Database created.");

            Debug.WriteLine("Creating tables...");
            db.CreateTable<ExerciseType>();
            db.CreateTable<User>();
            db.CreateTable<Workout>();
            db.CreateTable<Exercise>();
            db.CreateTable<ResistanceSet>();
            Debug.WriteLine("Tables created.");

            Debug.WriteLine("Connected to database.");
        }
        else
        {
            db = new SQLiteConnection(dbPath);
            if (db != null)
            {
                Debug.WriteLine("Connected to database.");
            }
            else
            {
                Debug.WriteLine("Could not connect to database.");
            }
        }
    }
}