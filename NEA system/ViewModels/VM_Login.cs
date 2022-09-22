using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NEA_system.ViewModels;

internal class VM_Login : VM_DbAccessor
{
    // Properties

    private ObservableCollection<User> users;
    public ObservableCollection<User> Users
    {
        get { return users; }
        set
        {
            users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    public Command UserSelectedCommand { get; }
    public Command GoToPage_CreateUser { get; }



    // Constructor

    public VM_Login()
    {
        UserSelectedCommand = new Command<User>(UserSelected);
        GoToPage_CreateUser = new Command(() => Shell.Current.GoToAsync($"{nameof(Page_CreateUser)}"));

        //FINISH
        //This method calls in the constrcutor but not in OnAppearing() on the page. Newly created users will therefore not show after returning from Page_CreateUser. Needs fixing!
        RefreshUsers();
    }



    // Methods

    public void RefreshUsers()
    {
        Users = new ObservableCollection<User>(db.Table<User>());
    }

    //Logs in instantly if the account has no associated password.
    private void UserSelected(User user)
    {
        //If this user does not have a password...
        if (user.PasswordHash == null)
        {
            //Proceed.
            //FINISH change this so that it passes the User object instead of just the ID. Then change the diagram on Lucidchart to follow the code.
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}?UserID={user.UserID}");
            Debug.WriteLine("LoginViewModel: Logged into user with UserID: " + user.UserID);
        }
        else
        {
            Shell.Current.GoToAsync($"{nameof(Page_EnterPassword)}?UserPasswordHash={user.PasswordHash}");
        }
    }
}