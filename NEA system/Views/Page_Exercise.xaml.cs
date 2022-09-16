using NEA_system.ViewModels;

namespace NEA_system.Views;

public partial class Page_Exercise : ContentPage
{
	public Page_Exercise()
	{
		InitializeComponent();
		BindingContext = new VM_Exercise();
	}
}