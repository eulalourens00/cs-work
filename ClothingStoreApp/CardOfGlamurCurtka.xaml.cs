namespace ClothingStoreApp;

public partial class CardOfGlamurCurtka : ContentPage
{
	public CardOfGlamurCurtka()
	{
        InitializeComponent();
	}

    private async void back_clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}