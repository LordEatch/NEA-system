using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Login : VM_Base
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
        foreach (var user in Session.DB.Table<User>().ToArray())
        {
            Users.Add(user);
        }
    }

    //Logs in instantly if the account has no associated password.
    private void UserSelected(User user)
    {
        //If this user does not have a password...
        if (user.PasswordHash == null)
        {
            Session.Login(user);
        }
        else
        {
            Shell.Current.GoToAsync($"{nameof(Page_EnterPassword)}", new Dictionary<string, object>() { ["User"] = user });
        }
    }
}