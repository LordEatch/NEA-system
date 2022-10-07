namespace NEA_system;

public partial class App : Application
{
    public User SessionUser;

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
