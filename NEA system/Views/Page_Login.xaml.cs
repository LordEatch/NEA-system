namespace NEA_system.Views;

public partial class Page_Login : ContentPage
{
	private readonly VM_Login VM;

	public Page_Login()
	{
		InitializeComponent();
		BindingContext = VM = new VM_Login();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		VM.LoadViewData();
    }
}