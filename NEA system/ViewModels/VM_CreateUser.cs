using System.Diagnostics;

namespace NEA_system.ViewModels;

internal class VM_CreateUser : VM_DbAccessor
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

    //FINISH
    private void InsertUser()
    {
        //FINISH
        //if (ValidateUsernameFormat)
        //if (ValidatePasswordFormat)

        var user = new User()
        {
            Username = Username,
            PasswordHash = User.CalculatePasswordHash(Password)
        };
        db.Insert(user);

        Debug.WriteLine($"User.CreateUser(): User created with id: '{user.UserID}', username: '{user.Username}' and password hash: '{user.PasswordHash}'.");
    }

    protected bool ValidateUsernameFormat()
    {
        return true;
    }

    protected bool ValidatePasswordFormat()
    {
        return true;
    }
}