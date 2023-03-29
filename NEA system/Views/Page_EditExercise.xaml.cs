namespace NEA_system.Views;

public partial class Page_EditExercise : ContentPage
{
    private readonly VM_EditExercise VM;

    public Page_EditExercise()
	{
		InitializeComponent();
		BindingContext = VM = new VM_EditExercise();
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
            //Native MAUI method that displays a popup to alert a user that their changes have been undone due to an empty entry.
            DisplayAlert("Alert", "Invalid input. Your changes to the exercise have been undone.", "OK");
        }

    }
}