namespace NEA_system.ViewModels;

[QueryProperty(nameof(MyUser), "User")]
internal class VM_EnterPassword
{
    // Properties

    public string Password { get; set; }
    public User MyUser { get; set; }

    public Command LoginCommand { get; }


    // Constructor

    public VM_EnterPassword()
    {
        LoginCommand = new Command(Login);
    }



    // Methods

    private void Login()
    {
        if (User.CalculatePasswordHash(Password) == MyUser.PasswordHash)
        {
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}",
                new Dictionary<string, object>
                {
                    ["User"] = MyUser
                });
        }
    }
}