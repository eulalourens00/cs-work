using WeatherViewer.viewmodels;
namespace WeatherViewer
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new WeatherViewModel();
        }

        
    }

}
