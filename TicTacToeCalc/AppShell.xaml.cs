namespace TicTacToeCalc
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //cry cry
            Routing.RegisterRoute("Calc", typeof(Calc));
            Routing.RegisterRoute("TicTacToe", typeof(TicTacToe));
        }
    }
}
