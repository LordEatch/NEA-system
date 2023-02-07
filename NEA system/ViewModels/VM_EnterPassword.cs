namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_EnterPassword : VM_Base
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
        //If the input is valid...
        if (Password != null)
        {
            //If the hash of this password matches that in the database...
            if (MyHash.HashPassword(Password) == MyUser.PasswordHash)
            {
                Session.Login(MyUser);
                return;
            }
        }

        ErrorMessage = "Incorrect password.";
    }
}