namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_Base, IDatabaseInput
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

    public bool ValidateInputFormat()
    {
        if (isPasswordProtected)
        {
            //Check both username and password.
            if (ValidateUsername() && ValidatePasswordFormat())
                return true;
        }
        else
        {
            //Check only username.
            if (ValidateUsername())
                return true;
        }

        return false;



        //  Kept separate in case I need to reuse somewhere else later.
        //  Example of modular programming. Methods defined within another method.

        bool ValidateUsername()
        {
            //If format is incorrect.
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = emptyEntryErrorMessage;
                return false;
            }
            //If the username exists...
            else if (!(Session.DB.Table<User>().Where(u => u.Username == Username).Count() == 0))
            {
                ErrorMessage = "That username already exists.";
                return false;
            }

            return true;
        }

        bool ValidatePasswordFormat()
        {
            //Allow white space for the password.
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = emptyEntryErrorMessage;
                return false;
            }
            else if (Password.Length > 30)
            {
                ErrorMessage = "Please use a password shorter than 30 characters.";
                return false;
            }

            return true;
        }
    }

    private void InsertUser()
    {
        if (!ValidateInputFormat())
            return;

        //Create a temporary user object.
        User user = new User()
        {
            Username = Username,
            IsPasswordProtected = false,
            PasswordHash = "N/A",
            LightMode = false
        };

        //If the user has chosen to use a password then append the user object.
        if (IsPasswordProtected)
        {
            user.IsPasswordProtected = true;
            user.PasswordHash = MyHash.HashPassword(Password);
        }

        Session.DB.Insert(user);
        System.Diagnostics.Debug.WriteLine($"User created with id: '{user.UserID}', username: '{user.Username}' and passwod hash: '{user.PasswordHash}'.");



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
    }
}