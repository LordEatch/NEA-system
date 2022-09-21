using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NEA_system.ViewModels
{
    internal class VM_Login : VM_DbAccessor
    {
        // Properties

        public ObservableCollection<User> Users { get; }

        public Command UserSelectedCommand { get; }



        // Constructor

        public VM_Login()
        {
            UserSelectedCommand = new Command<User>(UserSelected);

            Users = new ObservableCollection<User>(User.PullUsers(db));
        }



        // Methods

        //Logs in instantly if the account has no associated password.
        private void UserSelected(User user)
        {
            Debug.WriteLine("test");

            //If this user does not have a password...
            if (user.PasswordHash == null)
            {
                //Proceed.
                //test
                Debug.WriteLine("LoginViewModel: user.UserID: " + user.UserID);
                Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}?UserID={user.UserID}");
            }
            else
            {
                //Open a popup and pass this view model to it.
            }
        }
    }
}
