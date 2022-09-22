namespace NEA_system.Views;

public partial class Page_EnterPassword : ContentPage
{
	public Page_EnterPassword()
	{
		InitializeComponent();
		BindingContext = new VM_EnterPassword();
	}
}