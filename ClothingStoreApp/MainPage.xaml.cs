using System.Diagnostics;

namespace ClothingStoreApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void bagButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Bag", "In process", "OK");
        }

        private void menuButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Menu", "In process", "OK");
        }

        private async void change_CardOfOrder(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CardOfGlamurCurtka");
        }
    }
}
