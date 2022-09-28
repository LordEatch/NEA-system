namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_DbAccessor
{
    // Fields

    public string Username { get; set; }
    public string Password { get; set; }
    #region ErrorMessage
    private string errorMessage;
    public string ErrorMessage
    {
        get { return errorMessage; }
        protected set
        {
            errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
    #endregion

    public Command InsertUserCommand { get; }



    // Constructor

    public VM_CreateUser()
    {
        InsertUserCommand = new Command(InsertUser);
    }



    // Methods

    //FINISH
    private void InsertUser()
    {
        //FINISH
        //if (ValidateUsernameFormat)
        //if (ValidatePasswordFormat)



        //Check if this username already exists.
        if (CheckExistingUsernames())
        {
            ErrorMessage = "That username already exists.";
            return;
        }

        var user = new User()
        {
            Username = Username,
            PasswordHash = User.CalculatePasswordHash(Password)
        };
        db.Insert(user);

        System.Diagnostics.Debug.WriteLine($"User.CreateUser(): User created with id: '{user.UserID}', username: '{user.Username}' and password hash: '{user.PasswordHash}'.");

        Shell.Current.GoToAsync("..");
    }

    protected bool ValidateUsernameFormat()
    {
        return true;
    }

    protected bool ValidatePasswordFormat()
    {
        return true;
    }

    protected bool CheckExistingUsernames()
    {
        if (db.Table<User>().Where(u => u.Username == Username).Count() == 0)
            return false;
        else
            return true;
    }
}