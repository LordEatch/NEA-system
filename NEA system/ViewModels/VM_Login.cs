using NEA_system.Models;
using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Login : VM_Base, IDataDisplay
{
    // Properties

    public ObservableCollection<User> Users { get; set; }
    private User selectedUser;
    public User SelectedUser
    {
        get { return selectedUser; }
        set
        {
            System.Diagnostics.Debug.WriteLine("blah");

            selectedUser = value;

            if (selectedUser != null)
            {
                //If this user does not have a password...
                if (value.PasswordHash == null)
                {
                    Session.Login(value);
                }
                else
                {
                    Shell.Current.GoToAsync($"{nameof(Page_EnterPassword)}", new Dictionary<string, object>() { ["User"] = value });
                }
            }
        }
    }


    public Command GoToPage_CreateUser { get; }



    // Constructor

    public VM_Login()
    {
        GoToPage_CreateUser = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_CreateUser)}"));

        Users = new ObservableCollection<User>();
    }



    // Methods

    public void LoadViewData()
    {
        //Update user list.
        Users.Clear();
        foreach (var user in Session.DB.Table<User>())
        {
            Users.Add(user);
        }
    }
}