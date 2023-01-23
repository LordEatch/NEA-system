namespace NEA_system.ViewModels;

//When you want to implement username changing etc, inherit from IDataShower to display the old username etc...
internal class VM_ProfileSettings : VM_Base
{
    public Command DeleteUserCommand { get; set; }

    public VM_ProfileSettings()
    {
        DeleteUserCommand = new Command(DeleteUser);
    }

    //NOTE This does not delete all of the workouts, exercise types, exercises or sets created by this user.
    private void DeleteUser()
    {
        Session.DB.Delete<User>(Session.CurrentUser.UserID);
        Session.Logout();
    }
}