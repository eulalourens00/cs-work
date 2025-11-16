using System;
using Microsoft.Maui.Controls;
namespace ampleACD
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            CalculateAmpleFunction();
        }

        private void CalculateAmpleFunction()
        {
            if (!double.TryParse(EntryC.Text, out double c) || !double.TryParse(EntryD.Text, out double d))
            {
                DisplayAlert("Ошибка", "Пожалуйста, введите корректные числовые значения для c и d", "OK");
                return;
            }

            ClearResultsGrid();

            double s = 0;

            AddTableHeader();

            for (int i = 1; i <= 10; i++)
            {
                double a = (c + d) * i;
                double b = (c - d) * i;

                double ampleValue = Ample(a, b);

                s += ampleValue;

                AddTableRow(i, c, d, a, b, ampleValue);
            }

            double average = s / 10;

            SumLabel.Text = s.ToString("F4");
            AverageLabel.Text = average.ToString("F4");
            ResultsFrame.IsVisible = true;
        }

        private double Ample(double a, double b)
        {
            int intA = (int)Math.Floor(a);
            int intB = (int)Math.Floor(b);

            double sumOfSquares = Math.Pow(intA, 2) + Math.Pow(intB, 2);

            return Math.Sqrt(sumOfSquares);
        }

        private void ClearResultsGrid()
        {
            ResultsGrid.Children.Clear();
            ResultsGrid.RowDefinitions.Clear();

            ResultsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        private void AddTableHeader()
        {
            AddLabelToGrid(0, 0, "i", true);
            AddLabelToGrid(0, 1, "c", true);
            AddLabelToGrid(0, 2, "d", true);
            AddLabelToGrid(0, 3, "a", true);
            AddLabelToGrid(0, 4, "b", true);
            AddLabelToGrid(0, 5, "Ample(a,b)", true);
        }

        private void AddTableRow(int i, double c, double d, double a, double b, double ampleValue)
        {
            int row = i;

            ResultsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            AddLabelToGrid(row, 0, i.ToString(), false);
            AddLabelToGrid(row, 1, c.ToString("F1"), false);
            AddLabelToGrid(row, 2, d.ToString("F1"), false);
            AddLabelToGrid(row, 3, a.ToString("F2"), false);
            AddLabelToGrid(row, 4, b.ToString("F2"), false);
            AddLabelToGrid(row, 5, ampleValue.ToString("F4"), false);
        }

        private void AddLabelToGrid(int row, int column, string text, bool isHeader)
        {
            var label = new Label
            {
                Text = text,
                FontSize = isHeader ? 12 : 11,
                FontAttributes = isHeader ? FontAttributes.Bold : FontAttributes.None,
                TextColor = isHeader ? Color.FromArgb("#512BD4") : Colors.Black,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            ResultsGrid.Children.Add(label);
        }
    }

}
