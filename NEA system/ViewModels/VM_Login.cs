using System.Collections.ObjectModel;

namespace NEA_system.ViewModels;

internal class VM_Login : VM_Base, IDataDisplay
{
    // Properties

    public ObservableCollection<User> Users { get; set; }
    public User SelectedUser
    {
        get { return null; }
        set
        {
            if (value != null)
            {
                //If this user does not have a password...
                if (value.IsPasswordProtected == false)
                {
                    Session.Login(value);
                }
                else
                {
                    //Proceed to password entry.
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

        Users = new();
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