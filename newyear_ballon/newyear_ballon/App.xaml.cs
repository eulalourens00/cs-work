namespace newyear_ballon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();


        }
        protected override void OnStart()
        {
            base.OnStart();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Permissions.RequestAsync<Permissions.Photos>();
            });
        }
    }
}
