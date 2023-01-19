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
        VM.SaveData();
    }
}