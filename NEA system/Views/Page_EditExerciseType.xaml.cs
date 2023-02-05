namespace NEA_system.Views;

public partial class Page_EditExerciseType : ContentPage
{
	private VM_EditExerciseType VM;

	public Page_EditExerciseType()
	{
		InitializeComponent();
        BindingContext = VM = new VM_EditExerciseType();
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
            DisplayAlert("Alert", "You cannot leave empty entries. Your changes to the exercise type have been undone.", "OK");
        }
    }
}