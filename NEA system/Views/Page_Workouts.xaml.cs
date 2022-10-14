namespace NEA_system.Views;

public partial class Page_Workouts : ContentPage
{
	private readonly VM_Workouts VM;

	public Page_Workouts()
	{
		InitializeComponent();
		BindingContext = VM = new VM_Workouts();
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
        VM.RefreshPage();
    }
}