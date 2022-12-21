namespace NEA_system;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        Session.StartSession();
	}
}
