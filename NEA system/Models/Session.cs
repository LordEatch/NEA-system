namespace NEA_system.Models
{
    internal static class Session
    {
        public static User CurrentUser { get; set; }

        //More data eventually if needed...



        //FINISH
        public static void Login(User user)
        {
            CurrentUser = user;
            System.Diagnostics.Debug.WriteLine("Logged in with UserID: " + CurrentUser.UserID);
            Shell.Current.GoToAsync($"//{nameof(Page_Workouts)}");

            //FINISH make this work!
            //Shell.Current.FlyoutHeader = CurrentUser.Username;
        }

        //FINISH
        public static void Logout()
        {
            CurrentUser = null;
            System.Diagnostics.Debug.WriteLine("Logged out.");
            Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
            //FINISH make this work!
            //Shell.Current.FlyoutHeader = null;
        }
    }
}
