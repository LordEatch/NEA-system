namespace NEA_system.ViewModels;

internal class VM_ProfileSettings
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