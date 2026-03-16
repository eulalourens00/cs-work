namespace updatemarket;

public partial class ChangedNumberPage : ContentPage
{
    public ChangedNumberPage()
    {
        InitializeComponent();

    }
    private void OnMirrorClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputEntry.Text))
        {
            ResultLabel.Text = "Введите число!";
            ResultLabel.TextColor = Colors.Red;
            ResultLabel.IsVisible = true;
            return;
        }

        string digits = new string(InputEntry.Text.Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(digits))
        {
            ResultLabel.Text = "Только цифры!";
            ResultLabel.TextColor = Colors.Red;
            ResultLabel.IsVisible = true;
            return;
        }

        char[] charArray = digits.ToCharArray();
        Array.Reverse(charArray);
        string mirrored = new string(charArray);

        ResultLabel.Text = $" = {mirrored}";
        ResultLabel.TextColor = Colors.Green;
        ResultLabel.IsVisible = true;
    }
}