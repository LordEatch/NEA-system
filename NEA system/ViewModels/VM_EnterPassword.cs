namespace NEA_system.ViewModels;

[QueryProperty(nameof(UserPasswordHash), "UserPasswordHash")]
internal class VM_EnterPassword
{
    // Properties

    public string Password { get; set; }
    public string UserPasswordHash { get; set; }

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
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}");
        }
    }

    private bool ValidatePassword()
    {
        if (User.CalculatePasswordHash(Password) == UserPasswordHash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}