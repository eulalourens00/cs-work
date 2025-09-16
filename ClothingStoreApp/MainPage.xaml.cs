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
            bagButton_Cnsl.IsVisible = true;
        }

        private void menuButton_Clicked(object sender, EventArgs e)
        {
            menuButton_Cnsl.IsVisible = true;
        }

        private void bagCnsl_Clicked(object sender, EventArgs e)
        { bagButton_Cnsl.IsVisible = false; }

        private void manuCnsl_Clicked(object sender, EventArgs e)
        { menuButton_Cnsl.IsVisible = false; }
    }

}
