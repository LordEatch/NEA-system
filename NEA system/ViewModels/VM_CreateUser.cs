namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_Base
{
    //Properties

    public string Username { get; set; }
    public string Password { get; set; }
    private bool isPasswordProtected;
    public bool IsPasswordProtected
    {
        get { return isPasswordProtected; }
        set
        {
            isPasswordProtected = value;
            //Empty the password entry for disabling (cannot disable a filled entry).
            Password = null;
            //Update the entry so that it recognises that the bool is true/false and therefore enables/disables.
            OnPropertyChanged(Password);
        }
    }

    public Command InsertUserCommand { get; }



    // Constructor

    public VM_CreateUser()
    {
        InsertUserCommand = new Command(InsertUser);
    }



    // Methods

    private void InsertUser()
    {
        //Check if this username already exists.
        if (!ValidateUsername())
            return;

        //Create a temporary user object.
        User user = new User()
        {
            Username = Username,
            IsPasswordProtected = false,
            PasswordHash = 0,
            LightMode = false
        };

        //If the user has chosen to use a password then append the user object.
        if (IsPasswordProtected)
        {
            if (!ValidatePasswordFormat())
                return;

            user.IsPasswordProtected = true;
            user.PasswordHash = MyHash.CalculatePasswordHash(Password);
        }

        Session.DB.Insert(user);
        System.Diagnostics.Debug.WriteLine($"User created with id: '{user.UserID}', username: '{user.Username}'.");



        //Subscribe the user to the default exercise types.
        foreach (ExerciseType eT in Session.GetDefaultExerciseTypes())
        {
            var sub = new Subscription()
            {
                UserID = user.UserID,
                ExerciseTypeID = eT.ExerciseTypeID
            };
            Session.DB.Insert(sub);

            //test
            System.Diagnostics.Debug.WriteLine($"User subscribed to {eT.ExerciseTypeName}.");
        }

        //Login.
        Session.Login(user);



        bool ValidateUsername()
        {
            //If format is incorrect.
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Cannot use an empty username.";
                return false;
            }

            //If the username exists...
            if (!(Session.DB.Table<User>().Where(u => u.Username == Username).Count() == 0))
            {
                ErrorMessage = "That username already exists.";
                return false;
            }

            return true;
        }

        bool ValidatePasswordFormat()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Cannot use an empty password.";
                return false;
            }

            return true;
        }
    }
}