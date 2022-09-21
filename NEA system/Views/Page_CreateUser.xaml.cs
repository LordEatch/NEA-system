namespace NEA_system.Views;

public partial class Page_CreateUser : ContentPage
{
	public Page_CreateUser()
	{
		InitializeComponent();
		BindingContext = new VM_CreateUser();
	}
}