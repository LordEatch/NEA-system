namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_Input
{
    // Fields

    public string Username { get; set; }
    public string Password { get; set; }

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
        if (!ValidatePasswordFormat())
            return;

        var user = new User()
        {
            Username = Username,
            PasswordHash = MyHash.CalculatePasswordHash(Password),
            LightMode = false
        };
        Session.DB.Insert(user);



        //Subscribe the user to the default exercise types.
        foreach (ExerciseType eT in Session.GetDefaultExerciseTypes())
        {
            var sub = new Subscription()
            {
                UserID = Session.CurrentUser.UserID,
                ExerciseTypeID = eT.ExerciseTypeID
            };
            Session.DB.Insert(sub);

            //test
            System.Diagnostics.Debug.WriteLine($"User subscribed to {sub}.");
        }

        //test
        System.Diagnostics.Debug.WriteLine($"User created with id: '{user.UserID}', username: '{user.Username}' and password hash: '{user.PasswordHash}'.");

        Shell.Current.GoToAsync("..");
    }

    //FINISH
    private bool ValidatePasswordFormat()
    {
        return true;
    }

    private bool ValidateUsername()
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
}