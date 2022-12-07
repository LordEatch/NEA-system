namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_EnterPassword : VM_Input
{
    // Properties

    public User MyUser { get; set; }
    public string Password { get; set; }

    public Command LoginCommand { get; }


    // Constructor

    public VM_EnterPassword()
    {
        LoginCommand = new Command(Login);
    }



    // Methods

    private void Login()
    {
        if (ValidatePassword())
        {
            Session.Login(MyUser);
        }
    }

    private bool ValidatePassword()
    {
        if (MyHash.CalculatePasswordHash(Password) == MyUser.PasswordHash)
        {
            return true;
        }
        else
        {
            ErrorMessage = "Incorrect password.";
            return false;
        }
    }
}