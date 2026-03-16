using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using updatemarket.Models;
namespace updatemarket.ViewModels
{
    class CardPageViewModel: BaseViewModel
    {
        private Clothes _product;
        private bool _isFavorite;
        public Clothes Product
        {
            get => _product;
            set
            {
                if (SetProperty(ref _product, value))
                {
                    OnPropertyChanged(nameof(Product.Name));
                    OnPropertyChanged(nameof(Product.Color));
                    OnPropertyChanged(nameof(Product.Size));
                    OnPropertyChanged(nameof(Product.FormattedPrice));
                }
            }
        }
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }
        public ICommand GoBackCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand AddToCartCommand { get; }

        public CardPageViewModel()
        {
            GoBackCommand = new Command(OnGoBack);
            ToggleFavoriteCommand = new Command(OnToggleFavorite);
            AddToCartCommand = new Command(OnAddToCart);
        }

        public void Initialize(Clothes product)
        {
            Product = product;
            IsFavorite = false; 
        }

        private async void OnGoBack()
        {
            await Shell.Current.GoToAsync($"///MainPage");
        }

        private void OnToggleFavorite()
        {
            IsFavorite = !IsFavorite;
        }

        private async void OnAddToCart()
        {
            await Application.Current.MainPage.DisplayAlert(
                "Корзина",
                "Товар добавлен в корзину",
                "OK");
        }
    }
}
