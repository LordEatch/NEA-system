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
            //File.Delete(dbPath);

            //If the database file does not already exist...
            if (!File.Exists(dbPath))
            {
                Debug.WriteLine($"Creating new database at {dbPath}...");
                DB = new SQLiteConnection(dbPath);
                if (DB == null) Debug.WriteLine("Database could not connect.");
                Debug.WriteLine("Database created.");

                Debug.WriteLine("Creating tables...");
                DB.CreateTable<ExerciseType>();
                DB.CreateTable<User>();
                DB.CreateTable<Workout>();
                DB.CreateTable<Exercise>();
                DB.CreateTable<ResistanceSet>();
                Debug.WriteLine("Tables created.");

                Debug.WriteLine("Connected to database.");
            }
            else
            {
                DB = new SQLiteConnection(dbPath);
                if (DB != null)
                {
                    Debug.WriteLine("Connected to database.");
                }
                else
                {
                    Debug.WriteLine("Could not connect to database.");
                }
            }
        }

        //FINISH
        public static void Login(User user)
        {
            CurrentUser = user;
            Debug.WriteLine("Logged in with UserID: " + CurrentUser.UserID);
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}");

            //FINISH make this work!
            //Shell.Current.FlyoutHeader = CurrentUser.Username;
        }

        //FINISH
        public static void Logout()
        {
            CurrentUser = null;
            Debug.WriteLine("Logged out.");
            Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
            //FINISH make this work!
            //Shell.Current.FlyoutHeader = null;
        }
    }
}
