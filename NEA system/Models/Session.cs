using SQLite;
using System.Diagnostics;

namespace NEA_system.Models
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
        public static SQLiteConnection DB { get; private set; }
        private static readonly string fileName = "GymDatabase.db";



        public static void StartSession()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            Debug.WriteLine("Starting session at " + dbPath);

            //For when a new db is needed.
            //File.Delete(dbPath);

            //If the database file does not already exist...
            if (!File.Exists(dbPath))
            {
                Debug.WriteLine($"Creating new database at {dbPath}...");
                DB = new SQLiteConnection(dbPath);

                Debug.WriteLine("Creating tables...");
                DB.CreateTable<User>();
                DB.CreateTable<ExerciseType>();

                //Populate exercise type table with default exercises.
                foreach (ExerciseType eT in GetDefaultExerciseTypes())
                {
                    DB.Insert(eT);
                    //test
                    Debug.WriteLine($"{eT.ExerciseTypeName} added to exercise type table in local db.");
                }

                DB.CreateTable<Subscription>();
                DB.CreateTable<Workout>();
                DB.CreateTable<Exercise>();
                DB.CreateTable<ResistanceSet>();
                Debug.WriteLine("Tables created successfully.");
            }
            else
            {
                DB = new SQLiteConnection(dbPath);
            }
        }

        public static void Login(User user)
        {
            CurrentUser = user;

            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}");
            Debug.WriteLine("Logged in with UserID: " + CurrentUser.UserID);
        }

        public static void Logout()
        {
            CurrentUser = null;

            Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
            Debug.WriteLine("Logged out.");
        }

        //FINISH test . Make this pull from a default table of exercisetypes in json or an integrated db??
        public static ExerciseType[] GetDefaultExerciseTypes()
        {

            var y = new ExerciseType
            {
                ExerciseTypeID = 1,
                ExerciseTypeName = "Bench press",
                ExerciseTypeDescription = "Just push it innit."
            };

            ExerciseType[] x = new ExerciseType[] { y };

            return x;
        }

        public static ExerciseType[] GetSubscribedExerciseTypes()
        {
            //Query returns exercise types linked to a user via a record in the subscription table in alphabetical order.
            string query = @$"
                SELECT DISTINCT ExerciseType.ExerciseTypeID, ExerciseType.ExerciseTypeName, ExerciseType.ExerciseTypeDescription
                FROM ExerciseType
                INNER JOIN Subscription ON Subscription.ExerciseTypeID = ExerciseType.ExerciseTypeID
                WHERE Subscription.UserID = '{CurrentUser.UserID}'
                ORDER BY ExerciseType.ExerciseTypeName";

            var result = DB.Query<ExerciseType>(query).ToArray();

            Debug.WriteLine($"Session: {result.Length} workouts returned.");

            return result;
        }
    }
}