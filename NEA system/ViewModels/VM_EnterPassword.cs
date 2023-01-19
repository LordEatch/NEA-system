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
        //test
        if (MyHash.CalculatePasswordHash(Password) == MyUser.PasswordHash)
        {
            Session.Login(MyUser);
        }
        else
        {
            ErrorMessage = "Incorrect password.";
        }
    }
}