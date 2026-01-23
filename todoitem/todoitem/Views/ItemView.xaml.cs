using todoitem.Models;
using todoitem.ViewModels;

namespace todoitem.Views;

public partial class ItemView : ContentPage
{
	public ItemView()
	{
        InitializeComponent();

		viewModel.Navigation = Navigation;
		BindingContext = viewModel;
	}
}