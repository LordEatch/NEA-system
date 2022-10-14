namespace NEA_system.Views;

public partial class Page_FocusedWorkout : ContentPage
{
    private readonly VM_FocusedWorkout VM;

	public Page_FocusedWorkout()
	{
		InitializeComponent();
        BindingContext = VM = new VM_FocusedWorkout();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.RefreshPage();
    }
}