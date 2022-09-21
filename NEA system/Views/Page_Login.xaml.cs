namespace NEA_system.Views;

public partial class Page_Login : ContentPage
{
	public Page_Login()
	{
		InitializeComponent();
		BindingContext = new VM_Login();
	}
}