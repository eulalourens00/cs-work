namespace ClothingStoreApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //МНЕ НИЧЕГО НЕ НУЖНО ТОЛЬКО ПЛАЧЬ
            //    Я РЫДААЮЮЮЮЮЮЮЮЮЮЮЮЮЮЮ

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("CardOfGlamurCurtka", typeof(CardOfGlamurCurtka));
        }
    }
}
