using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using updatemarket.Models;
namespace updatemarket.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Clothes> _popularProducts;
        private Clothes _selectedProduct;

        public MainPageViewModel()
        {
            LoadProducts();
            ProductSelectedCommand = new Command<Clothes>(OnProductSelected);
        }

        public ObservableCollection<Clothes> PopularProducts
        {
            get => _popularProducts;
            set => SetProperty(ref _popularProducts, value);
        }

        public Clothes SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value) && value != null)
                {
                    ProductSelectedCommand?.Execute(value);
                }
            }
        }

        public ICommand ProductSelectedCommand { get; }

        private void LoadProducts()
        {
            PopularProducts = new ObservableCollection<Clothes>
            {
                new Clothes
                {
                    Name = "Avoine Hooded Quilted Jacket",
                    Price = 1500,
                    Rating = 4.7,
                    ImageUrl = "kurtka.png",
                    Color = "BEIGE",
                    Size = "MEDIUM"
                },
                new Clothes
                {
                    Name = "Hooded Metallic Shell Jacket",
                    Price = 1050,
                    Rating = 4.7,
                    ImageUrl = "glamurkurtka.png",
                    Color = "BLACK",
                    Size = "LARGE"
                }
            };
        }

        private async void OnProductSelected(Clothes product)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product }
            };

            await Shell.Current.GoToAsync($"///CardPage", navigationParameter);
        }
    }
}
