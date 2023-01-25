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

		if (VM.ValidateInputFormat())
		{
            VM.SaveData();
        }
		else
		{
			//Native MAUI method that displays a popup to alert a user that their changes have been undone due to an empty workout muscle group.
            DisplayAlert("Alert", "You cannot leave workout muscle group as empty. Your changes to the workout (but not exercises) have been undone.", "OK");
        }
	}
}