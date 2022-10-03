using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NEA_system.ViewModels;

internal class VM_Login : VM_DbAccessor
{
    // Properties

    public ObservableCollection<User> Users { get; set; }


    public Command UserSelectedCommand { get; }
    public Command GoToPage_CreateUser { get; }



    // Constructor

    public VM_Login()
    {
        UserSelectedCommand = new Command<User>(UserSelected);
        GoToPage_CreateUser = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_CreateUser)}"));

        Users = new ObservableCollection<User>();
    }



    // Methods

    public void RefreshUsers()
    {
        Users.Clear();
        foreach (var user in db.Table<User>().ToArray())
        {
            Users.Add(user);
        }
    }


    //FINISH
    //Logs in instantly if the account has no associated password.
    private void UserSelected(User user)
    {
        //If this user does not have a password...
        if (user.PasswordHash == null)
        {
            //Proceed.
            //FINISH change this so that it passes the User object instead of just the ID. Then change the diagram on Lucidchart to follow the code.
            
            //Pass user to shell instead for session type login?
            
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}?UserID={user.UserID}");
            Debug.WriteLine("VM_Login: Logged into user with UserID: " + user.UserID);
        }
        else
        {
            Shell.Current.GoToAsync($"{nameof(Page_EnterPassword)}?UserPasswordHash={user.PasswordHash}");
        }
    }
}