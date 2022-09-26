namespace NEA_system.Views;

public partial class Page_Workouts : ContentPage
{
	private VM_Workouts vm;

	public Page_Workouts()
	{
		InitializeComponent();
		BindingContext = vm = new VM_Workouts();
	}

	//Needs to be aync because of a MAUI bug where the query parameters (UserID) are not passed to the page before OnAppearing() is called.
	protected async override void OnAppearing()
	{
        base.OnAppearing();
        await Task.Yield();
        vm.RefreshWorkouts();
    }
}