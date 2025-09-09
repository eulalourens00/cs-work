namespace Main
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            grid.Add(new BoxView { Color = Color.FromRgb(25, 235, 25) }, 0, 0);
            grid.Add(new BoxView { Color = Color.FromRgba(244, 201, 223, 122) }, 0, 1);
            grid.Add(new BoxView { Color = Color.FromRgba(55, 225, 215, 132) }, 1, 0);

            grid.Add(new BoxView { Color = Color.FromRgba(111, 211, 215, 24) }, 1, 1);
            grid.Add(new BoxView { Color = Color.FromRgba(14, 122, 132, 54) }, 2, 0);
            grid.Add(new BoxView { Color = Color.FromRgba(209, 202, 234, 12) }, 2, 1);

            Content = grid;

            //InitializeComponent();
        }
       
    }

}
