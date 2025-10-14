using System.Diagnostics;

namespace date_maui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            label.Text = $" Value {e.Value}";
        }

        private void statusCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            statusLabel.Text = $"Status: {(e.Value ? "married man/married woman" : "married woman/married man")}";
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton selectedRadioButton = ((RadioButton)sender);
            if (header != null) { header.Text = $" Language - {selectedRadioButton.Value}"; }
        }

        private void langPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            header.Text = $"Choosed the way - {langPicker.SelectedItem}";
        }
    }

}
