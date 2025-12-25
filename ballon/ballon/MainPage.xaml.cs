namespace ballon
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, Color> colors = new()
        {
            { "Red", Colors.Red },
            { "Blue", Colors.Blue },
            { "Green", Colors.Green },
            { "Yellow", Colors.Yellow },
            { "White", Colors.White },
            { "Pink", Colors.Pink }
        };

        private readonly Dictionary<string, string> patterns = new()
        {
            { "stripes", "stripers.png" },
            { "snowflake", "snowflake.jpg" },
            { "star", "star.png" }
        };


        private Color currentColor = Colors.Red;

        private string currentPattern = "none";

        public MainPage()
        {
            InitializeComponent();
            OpacityLabel.Text = $"{(int)(OpacitySlider.Value * 100)}%";
        }

        private void OnShapeChanged(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string shape = button.CommandParameter.ToString();

                switch (shape)
                {
                    case "Circle":
                        BallContainer.CornerRadius = 100;
                        BallContainer.WidthRequest = 200;
                        BallContainer.HeightRequest = 200;
                        break;
                    case "Drop":
                        BallContainer.CornerRadius = 100;
                        BallContainer.WidthRequest = 180;
                        BallContainer.HeightRequest = 220;
                        break;
                    case "Oval":
                        BallContainer.CornerRadius = 100;
                        BallContainer.WidthRequest = 160;
                        BallContainer.HeightRequest = 240;
                        break;
                }
            }
        }

        private void OnColorChanged(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string colorName = button.CommandParameter.ToString();
                if (colors.TryGetValue(colorName, out var color))
                {
                    currentColor = color;
                    BallContainer.BackgroundColor = color;
                }
            }
        }

        private void OnPatternChanged(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string pattern = button.CommandParameter.ToString();
                currentPattern = pattern;

                if (pattern == "none")
                {
                    PatternLabel.IsVisible = false;
                }
                else
                {
                    PatternLabel.Text = pattern switch
                    {
                        "stripes" => "🎄", 
                        "snowflake" => "❄️",
                        "star" => "⭐",
                        _ => ""
                    };
                    PatternLabel.IsVisible = true;
                }
            }
        }

        private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
        {
            PatternLabel.Opacity = e.NewValue;
            OpacityLabel.Text = $"{(int)(e.NewValue * 100)}%";
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var stream = await BallContainer.CaptureAsync();

                var status = await Permissions.RequestAsync<Permissions.Photos>();

                if (status == PermissionStatus.Granted)
                {
                    var file = Path.Combine(FileSystem.AppDataDirectory, $"christmas_ball_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                    using (var fileStream = File.OpenWrite(file))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    SaveMessage.Text = "Шарик сохранен!";
                    SaveMessage.IsVisible = true;

                    await Task.Delay(3000);
                    SaveMessage.IsVisible = false;

                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Новогодний шарик",
                        File = new ShareFile(file)
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось сохранить: {ex.Message}", "OK");
            }
        }
    }

}
