namespace NEA_system.Views;

public partial class Page_Workouts : ContentPage
{
	public Page_Workouts()
	{
		InitializeComponent();
		BindingContext = new VM_Workouts();
	}
}