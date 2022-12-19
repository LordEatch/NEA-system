using NEA_system.Themes;

namespace NEA_system;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        //test
        ICollection<ResourceDictionary> mergedDictionaries = Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new Theme_Dark());
        }

        Session.StartSession();
	}
}
