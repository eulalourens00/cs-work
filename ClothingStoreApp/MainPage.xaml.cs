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
            debugLabel1.Text = "Кнопка нажата!";
        }

        private void menuButton_Clicked(object sender, EventArgs e)
        {
            debugLabel2.Text = "Кнопка нажата!";
        }
    }

}
