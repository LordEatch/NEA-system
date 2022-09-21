namespace NEA_system;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();



		// Routes

		Routing.RegisterRoute(nameof(Page_CreateUser), typeof(Page_CreateUser));
	}
}
