namespace TicTacToeCalc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Calc_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Calc");
        }

        private async void TTT_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("TicTacToe");
        }

        //помогай, я умираю
        //    я не буду я не буду

    }

}
