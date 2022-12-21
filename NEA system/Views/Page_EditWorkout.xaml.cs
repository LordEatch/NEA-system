namespace NEA_system.Views;

public partial class Page_EditWorkout : ContentPage
{
	private readonly VM_EditWorkout VM;

	public Page_EditWorkout()
	{
		InitializeComponent();
		BindingContext = VM = new VM_EditWorkout();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.LoadViewData();
    }

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		VM.SaveData();
	}
}