using NEA_system.ViewModels;

namespace NEA_system.Views;

public partial class Page_Exercise : ContentPage
{
	private VM_Exercise vm;

	public Page_Exercise()
	{
		InitializeComponent();
		BindingContext = vm = new VM_Exercise();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		vm.UpdateSets();
	}
}