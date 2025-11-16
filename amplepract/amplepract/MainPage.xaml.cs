using System.Text;
namespace amplepract
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            ClearResults();
        }

        private void OnDecimalNumberChanged(object sender, TextChangedEventArgs e)
        {
            ConvertNumber();
        }

        private void OnQuickInputClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                DecimalEntry.Text = button.Text;
            }
        }

        private void ConvertNumber()
        {
            string input = DecimalEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                ClearResults();
                HideError();
                return;
            }

            // Проверяем, является ли ввод числом
            if (int.TryParse(input, out int decimalNumber))
            {
                HideError();
                UpdateResults(decimalNumber);
            }
            else
            {
                ShowError("Пожалуйста, введите целое число");
                ClearResults();
            }
        }

        private void UpdateResults(int decimalNumber)
        {
            BinaryResult.Text = DecimalToBinary(decimalNumber);
            OctalResult.Text = DecimalToOctal(decimalNumber);
            HexadecimalResult.Text = DecimalToHexadecimal(decimalNumber);

            // Добавляем визуальное оформление для результатов
            BinaryResult.TextColor = Colors.DarkGreen;
            OctalResult.TextColor = Colors.DarkBlue;
            HexadecimalResult.TextColor = Colors.DarkRed;
        }

        private void ClearResults()
        {
            BinaryResult.Text = "—";
            OctalResult.Text = "—";
            HexadecimalResult.Text = "—";

            BinaryResult.TextColor = Colors.Gray;
            OctalResult.TextColor = Colors.Gray;
            HexadecimalResult.TextColor = Colors.Gray;
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }

        private void HideError()
        {
            ErrorLabel.IsVisible = false;
        }

        // Конвертация в двоичную систему
        private string DecimalToBinary(int decimalNumber)
        {
            if (decimalNumber == 0) return "0";

            bool isNegative = decimalNumber < 0;
            int number = Math.Abs(decimalNumber);

            StringBuilder binary = new StringBuilder();

            while (number > 0)
            {
                binary.Insert(0, number % 2);
                number /= 2;
            }

            return isNegative ? $"-{binary}" : binary.ToString();
        }

        // Конвертация в восьмеричную систему
        private string DecimalToOctal(int decimalNumber)
        {
            if (decimalNumber == 0) return "0";

            bool isNegative = decimalNumber < 0;
            int number = Math.Abs(decimalNumber);

            StringBuilder octal = new StringBuilder();

            while (number > 0)
            {
                octal.Insert(0, number % 8);
                number /= 8;
            }

            return isNegative ? $"-{octal}" : octal.ToString();
        }

        // Конвертация в шестнадцатеричную систему
        private string DecimalToHexadecimal(int decimalNumber)
        {
            if (decimalNumber == 0) return "0";

            bool isNegative = decimalNumber < 0;
            int number = Math.Abs(decimalNumber);

            string hexChars = "0123456789ABCDEF";
            StringBuilder hexadecimal = new StringBuilder();

            while (number > 0)
            {
                hexadecimal.Insert(0, hexChars[number % 16]);
                number /= 16;
            }

            return isNegative ? $"-{hexadecimal}" : hexadecimal.ToString();
        }
    }
}


