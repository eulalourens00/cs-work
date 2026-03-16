using updatemarket.Models;
using updatemarket.ViewModels;

namespace updatemarket;


[QueryProperty(nameof(Product), "Product")]
public partial class CardPage : ContentPage
{
    private CardPageViewModel _viewModel;
    private Clothes _product;

    public Clothes Product
    {
        get => _product;
        set
        {
            _product = value;
            System.Diagnostics.Debug.WriteLine($" Товар получен: {value?.Name}");
            if (_viewModel != null)
            {
                _viewModel.Initialize(value);
            }
        }
    }

    public CardPage()
	{
		InitializeComponent();
        _viewModel = (CardPageViewModel)BindingContext;

        if (_product != null)
        {
            _viewModel.Initialize(_product);
        }
    }

   
}