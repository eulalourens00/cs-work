using System.Diagnostics;

namespace date_maui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Dateselected(object sender, DateChangedEventArgs e)
        {
            Debug.WriteLine(label1 is { });
            Debug.WriteLine(typeof(Label));
            if(label1 is { })
            {
                label1.Text = $"You choosed: {e.NewDate.ToString("d")}";
            }
        }
    }

}
