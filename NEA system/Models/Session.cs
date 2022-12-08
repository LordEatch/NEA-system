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

            //For when a new db is needed.
            File.Delete(dbPath);

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
                    Debug.WriteLine($"{eT} added to exercise type table in local db.");
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

        //FINISH bad url
        public static ExerciseType[] GetDefaultExerciseTypes()
        {
            //SQLiteConnection tempConn = new SQLiteConnection(@"ExerciseTypes.db");
            //return tempConn.Table<ExerciseType>().ToArray();
            return new ExerciseType[] { };
        }

        //FINISH
        public static void Login(User user)
        {
            CurrentUser = user;

            //FINISH make this work!
            //Shell.Current.FlyoutHeader = CurrentUser.Username;

            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}");
            Debug.WriteLine("Logged in with UserID: " + CurrentUser.UserID);
        }

        //FINISH
        public static void Logout()
        {
            CurrentUser = null;

            //FINISH make this work!
            //Shell.Current.FlyoutHeader = null;

            Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
            Debug.WriteLine("Logged out.");
        }
    }
}
