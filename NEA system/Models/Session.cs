using SQLite;
using System.Diagnostics;

namespace NEA_system.Models
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
        private static SQLiteConnection DB;
        private static readonly string fileName = "GymDatabase.db";



        public static void StartSession()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            Debug.WriteLine("SESSION: Starting session at " + dbPath);

            //For when a new db is needed.
            //File.Delete(dbPath);

            //If the database file does not already exist...
            if (!File.Exists(dbPath))
            {
                Debug.WriteLine($"SESSION: Creating new database at {dbPath}...");
                DB = new SQLiteConnection(dbPath);

                Debug.WriteLine("SESSION: Creating tables...");
                //SQLite-net creates a schema for the database based off of the classes used as tables.
                DB.CreateTable<User>();
                DB.CreateTable<ExerciseType>();

                //Populate exercise type table with default exercises.
                foreach (ExerciseType eT in GetDefaultExerciseTypes())
                {
                    DB.Insert(eT);
                    Debug.WriteLine($"SESSION: {eT.ExerciseTypeName} added to exercise type table in local db.");
                }

                DB.CreateTable<Subscription>();
                DB.CreateTable<Workout>();
                DB.CreateTable<Exercise>();
                DB.CreateTable<ResistanceSet>();
                Debug.WriteLine("SESSION: Tables created successfully.");
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
            Debug.WriteLine("SESSION: Logged in with UserID: " + CurrentUser.UserID);
        }

        public static void Logout()
        {
            CurrentUser = null;

            Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
            Debug.WriteLine("SESSION: Logged out.");
        }



        //In future, for delete methods, make sure to delete all child objects aswell. For example, delete all sets within an exercise when deleting the exercise.

        #region User CRUD methods
        // Create

        public static int InsertUser(User user)
        {
            DB.Insert(user);
            return user.UserID;
        }



        // Read

        //Get all users on the database.
        public static User[] GetUsers()
        {
            return DB.Table<User>().ToArray();
        }

        //Return a list of users that match a given a username.
        public static User[] GetUsersByUsername(string username)
        {
            return DB.Table<User>().Where(u => u.Username == username).ToArray();
        }



        // Delete

        public static int DeleteUser(int userID)
        {
            DB.Delete<User>(userID);
            Debug.WriteLine($"SESSION: User with id:{userID} deleted.");
            return userID;
        }
        #endregion



        #region ExerciseType CRUD methods

        // Create

        public static int InsertExerciseType(ExerciseType exerciseType)
        {
            DB.Insert(exerciseType);
            return exerciseType.ExerciseTypeID;
        }



        // Read

        public static ExerciseType GetExerciseType(int exerciseTypeID)
        {
            return DB.Table<ExerciseType>().Where(eT => eT.ExerciseTypeID == exerciseTypeID).FirstOrDefault();
        }

        //Get a list of hard-coded default exercises.
        public static ExerciseType[] GetDefaultExerciseTypes()
        {
            var exerciseType = new ExerciseType
            {
                ExerciseTypeID = 1,
                ExerciseTypeName = "Bench press",
                ExerciseTypeDescription = "Push a barbell up while laying down."
            };

            ExerciseType[] exerciseTypeArray = new ExerciseType[] { exerciseType };

            return exerciseTypeArray;
        }

        //Get all exercise types that the current user is subscribed to.
        public static ExerciseType[] GetSubscribedExerciseTypes()
        {
            //Query returns exercise types linked to a user via a record in the subscription table in alphabetical order.
            string query = @$"
                SELECT ExerciseType.ExerciseTypeID, ExerciseTypeName, ExerciseTypeDescription
                FROM (User
                INNER JOIN Subscription
                ON User.UserID = Subscription.UserID)
                INNER JOIN ExerciseType
                ON Subscription.ExerciseTypeID = ExerciseType.ExerciseTypeID
                WHERE User.UserID = {CurrentUser.UserID}
                ORDER BY ExerciseType.ExerciseTypeName";

            var result = DB.Query<ExerciseType>(query).ToArray();

            Debug.WriteLine($"SESSION: {result.Length} exercise types returned.");

            return result;
        }

        public static bool ExerciseTypeNameExists(string exerciseTypeName)
        {
            foreach (ExerciseType exerciseType in GetSubscribedExerciseTypes())
            {
                //If the exercise type already exists...
                if (exerciseType.ExerciseTypeName == exerciseTypeName)
                    return true;
            }

            return false;
        }



        // Update

        public static int UpdateExerciseType(ExerciseType exerciseType)
        {
            DB.Update(exerciseType);
            Debug.WriteLine($"SESSION: Exercise type with id:{exerciseType.ExerciseTypeID} has been updated.");
            return exerciseType.ExerciseTypeID;
        }
        #endregion



        #region Subscription CRUD methods

        // Create

        public static int InsertSubscription(Subscription subscription)
        {
            DB.Insert(subscription);
            return subscription.SubscriptionID;
        }



        // Read

        //Match the current user's ID and a given exercise type ID to get the relevant subscription.
        public static Subscription GetSubscriptionByExerciseType(int exerciseTypeID)
        {
            return DB.Table<Subscription>().Where(s => (s.ExerciseTypeID == exerciseTypeID) && (s.UserID == CurrentUser.UserID)).FirstOrDefault();
        }



        // Delete

        public static int DeleteSubscription(int subscriptionID)
        {
            DB.Delete<Subscription>(subscriptionID);
            Debug.WriteLine($"SESSION: Subscription with id:{subscriptionID} deleted.");
            return subscriptionID;
        }

        #endregion



        #region Workout CRUD methods
        // Create

        public static int InsertWorkout(Workout workout)
        {
            DB.Insert(workout);
            return workout.WorkoutID;
        }



        // Read

        //Get all of a user's workouts. FULL OUTER JOIN will find matching and unmatching entries. Useful for showing empty workouts that will have no exercises yet.
        public static Workout[] GetWorkouts(string filter)
        {
            string query = @$"
                SELECT DISTINCT Workout.WorkoutId, UserID, Date, WorkoutMuscleGroup, WorkoutComment
                FROM (ExerciseType
                INNER JOIN Exercise
                ON ExerciseType.ExerciseTypeID = Exercise.ExerciseTypeID)
                FULL OUTER JOIN Workout
                ON Exercise.WorkoutID = Workout.WorkoutID
				WHERE Workout.UserID = '{CurrentUser.UserID}'
				AND (ExerciseTypeName LIKE '%{filter}%'
                OR Workout.Date LIKE '%{filter}%'
                OR Workout.WorkoutMuscleGroup LIKE '%{filter}%'
                OR WorkoutComment LIKE '%{filter}%')
                ORDER BY Date DESC, Workout.WorkoutID DESC";

            return DB.Query<Workout>(query).ToArray();
        }



        // Update

        public static int UpdateWorkout(Workout workout)
        {
            DB.Update(workout);
            Debug.WriteLine($"SESSION: Workout with id:{workout.WorkoutID} has been updated.");
            return workout.WorkoutID;
        }



        // Delete

        public static int DeleteWorkout(int workoutID)
        {
            DB.Delete<Workout>(workoutID);
            Debug.WriteLine($"SESSION: Workout with id:{workoutID} deleted.");
            return workoutID;
        }
        #endregion



        #region Exercise CRUD methods

        // Create

        public static int InsertExercise(Exercise exercise)
        {
            DB.Insert(exercise);
            return exercise.ExerciseID;
        }



        // Read

        //Return an array of all exercises from a given workout.
        public static Exercise[] GetExercisesByWorkout(int workoutID)
        {
            return DB.Table<Exercise>().Where(e => e.WorkoutID == workoutID).ToArray();
        }



        // Update

        public static int UpdateExercise(Exercise exercise)
        {
            DB.Update(exercise);
            Debug.WriteLine($"SESSION: Exercise with id:{exercise.ExerciseID} has been updated.");
            return exercise.ExerciseID;
        }



        // Delete

        public static int DeleteExercise(int exerciseID)
        {
            DB.Delete<Exercise>(exerciseID);
            return exerciseID;
        }

        #endregion



        #region ResistanceSet CRUD methods

        //Create

        public static int InsertResistanceSet(ResistanceSet resistanceSet)
        {
            DB.Insert(resistanceSet);
            Debug.WriteLine($"SESSION: Set with id:{resistanceSet.SetID}, ExerciseID:{resistanceSet.ExerciseID} inserted.");
            return resistanceSet.SetID;
        }



        // Read

        public static ResistanceSet GetResistanceSet(int resistanceSetID)
        {
            return DB.Table<ResistanceSet>().Where(rS => rS.SetID == resistanceSetID).FirstOrDefault();
        }

        //Get the an array of sets of a given exercise type from newest to oldest.
        public static ResistanceSet[] GetResistanceSetsByExerciseType(int exerciseTypeID)
        {
            string query = @$"
                SELECT SetID, ResistanceSet.ExerciseID, Mass, StrictReps, CheatedReps, SetComment
                FROM ((ResistanceSet
                INNER JOIN Exercise
                ON ResistanceSet.ExerciseID = Exercise.ExerciseID)
                INNER JOIN ExerciseType
                ON ExerciseType.ExerciseTypeID = Exercise.ExerciseTypeID)
                INNER JOIN Workout
                ON Workout.WorkoutID = Exercise.WorkoutID
                WHERE Workout.UserID = {CurrentUser.UserID}
                AND Exercise.ExerciseTypeID = {exerciseTypeID}
                ORDER BY Workout.Date DESC, ResistanceSet.SetID DESC";

            return DB.Query<ResistanceSet>(query).ToArray();
        }

        //Get all of the exercises within a given exercise.
        public static ResistanceSet[] GetResistanceSetsByExercise(int exerciseID)
        {
            return DB.Table<ResistanceSet>().Where(rS => rS.ExerciseID == exerciseID).ToArray();
        }



        // Update

        public static int UpdateResistanceSet(ResistanceSet resistanceSet)
        {
            DB.Update(resistanceSet);
            Debug.WriteLine($"SESSION: Set with id:{resistanceSet.SetID}, ExerciseID:{resistanceSet.ExerciseID} has been updated.");
            return resistanceSet.SetID;
        }



        // Delete

        public static int DeleteResistanceSet(int resistanceSetID)
        {
            DB.Delete<ResistanceSet>(resistanceSetID);
            return resistanceSetID;
        }

        #endregion
    }
}