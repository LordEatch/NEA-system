namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_Base
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
        if (UsernameExists())
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

        //test
        System.Diagnostics.Debug.WriteLine($"User created with id: '{user.UserID}', username: '{user.Username}' and password hash: '{user.PasswordHash}'.");

        Shell.Current.GoToAsync("..");
    }

    //FINISH
    private bool ValidatePasswordFormat()
    {
        return true;
    }

    private bool UsernameExists()
    {
        if (Session.DB.Table<User>().Where(u => u.Username == Username).Count() == 0)
        {
            return false;
        }
        else
        {
            ErrorMessage = "That username already exists.";
            return true;
        }
    }
}