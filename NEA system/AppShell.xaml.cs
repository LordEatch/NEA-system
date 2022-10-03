namespace NEA_system;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Routes

		Routing.RegisterRoute(nameof(Page_CreateUser), typeof(Page_CreateUser));
        Routing.RegisterRoute(nameof(Page_EnterPassword), typeof(Page_EnterPassword));
    }

	//FINISH does this break MVVM by having navigation in a view?
	private void LogoutClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//{nameof(Page_Login)}");
	}
}
