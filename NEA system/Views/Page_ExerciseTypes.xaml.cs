namespace NEA_system.Views;

public partial class Page_ExerciseTypes : ContentPage
{
    private readonly VM_ExerciseTypes VM;

    public Page_ExerciseTypes()
    {
        InitializeComponent();
        BindingContext = VM = new VM_ExerciseTypes();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.LoadViewData();
    }
}