namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_EnterPassword : VM_Base
{
    // Properties

    public User MyUser { get; set; }
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
        //Wait for 1/4 of a second to prevent brute forcing.
        Thread.Sleep(250);

        if (User.CalculatePasswordHash(Password) == MyUser.PasswordHash)
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