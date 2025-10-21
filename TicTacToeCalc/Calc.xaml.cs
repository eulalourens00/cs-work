namespace TicTacToeCalc;

public partial class Calc : ContentPage
{
	public Calc()
	{
		InitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }

    private void Solve_Clicked(object sender, EventArgs e)
    {
        if(double.TryParse(A.Text, out double a) &&
            double.TryParse(B.Text, out double b) &&
            double.TryParse(C.Text, out double c))
        {
            string info = string.Empty;

            var discriminant = (b * b) - (4 * a * c);
            info += $"Discriminant = {discriminant}\n\n";

            if (discriminant > 0)
            {
                var x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                var x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                info += $"Two real roots:\n";
                info += $"x1 =  {x1:F2}\n";  /*F2 = X,00 */
                info += $"x2 =  {x2:F2}";
            }
            else if (discriminant == 0)
            {
                var x = -b / (2 * a);
                info += $"One real root:\n";
                info += $"x =  {x:F2}";
            }
            else
            {
                var realPart = -b / (2 * a);
                var imaginaryPart = Math.Sqrt(-discriminant) / (2 * a);
                info += $"Complex roots:\n";
                info += $"x1 = {realPart:F2} + {imaginaryPart:F2}i\n";
                info += $"x2 = {realPart:F2} - {imaginaryPart:F2}i";
            }

            Res.Text = info;
            Res.TextColor = Colors.White;
        }
        else
        {
            Res.Text = "Something is wrong. Check your values.";
        }
    }
}