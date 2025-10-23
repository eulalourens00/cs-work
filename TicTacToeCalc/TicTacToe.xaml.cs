namespace TicTacToeCalc;

public partial class TicTacToe : ContentPage
{
    private Button[,] cells;
    private bool isXTurn = true;
    private int moveCount = 0;
    private bool gameEnded = false;
    public TicTacToe()
	{
		InitializeComponent();
        StartGame();
	}

    private void StartGame()
    {
        cells = new Button[3, 3];

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var button = new Button
                {
                    BackgroundColor = Color.FromArgb("#2C3E50"),
                    TextColor = Colors.White,
                    FontSize = 40,
                    FontAttributes = FontAttributes.Bold,
                    Text="",
                    CornerRadius = 10,
                    Margin = 2
                };

                button.Clicked += OnCellClicked;

                GameGrid.Children.Add(button);
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                cells[row, col] = button;
            }
        }

        ResetGame();
    }

    private void OnCellClicked(object sender, EventArgs e)
    {
        if (gameEnded) return;

        var button = (Button)sender;

        if (!string.IsNullOrEmpty(button.Text))
            return;

        button.Text = isXTurn ? "X" : "O";
        button.TextColor = isXTurn ? Color.FromArgb("#E74C3C") : Color.FromArgb("#3498DB");

        moveCount++;

        if (CheckWinner())
        {
            Winning();
            gameEnded = true;
        }
        else if (moveCount == 9)
        {
            gameEnded = true;
        }
        else
        {
            // свапаем
            isXTurn = !isXTurn;
        }
    }

    private bool CheckWinner()
    {
        string currentPlayer = isXTurn ? "X" : "O";

        // строки
        for (int row = 0; row < 3; row++)
        {
            if (cells[row, 0].Text == currentPlayer &&
                cells[row, 1].Text == currentPlayer &&
                cells[row, 2].Text == currentPlayer)
                return true;
        }

        // столбцы
        for (int col = 0; col < 3; col++)
        {
            if (cells[0, col].Text == currentPlayer &&
                cells[1, col].Text == currentPlayer &&
                cells[2, col].Text == currentPlayer)
                return true;
        }

        // диагонали
        if (cells[0, 0].Text == currentPlayer &&
            cells[1, 1].Text == currentPlayer &&
            cells[2, 2].Text == currentPlayer)
            return true;

        if (cells[0, 2].Text == currentPlayer &&
            cells[1, 1].Text == currentPlayer &&
            cells[2, 0].Text == currentPlayer)
            return true;

        return false;
    }
    // подсветочка
     //боже оно подсвечивает все элементы победителя ааа
     // исправить позже
    private void Winning()
    {
        string winner = isXTurn ? "X" : "O";

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (cells[row, col].Text == winner)
                {
                    cells[row, col].BackgroundColor = Color.FromArgb("#27AE60");
                }
            }
        }
    }

    //заново емае
    private void ResetGame()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                cells[row, col].Text = "";
                cells[row, col].BackgroundColor = Color.FromArgb("#2C3E50");
                cells[row, col].IsEnabled = true;
            }
        }

        isXTurn = true;
        moveCount = 0;
        gameEnded = false;
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        ResetGame();
    }

}