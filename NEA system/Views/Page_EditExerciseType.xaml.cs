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
}