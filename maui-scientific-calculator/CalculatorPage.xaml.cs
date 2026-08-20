namespace goruntu_prog_odev;

public partial class CalculatorPage : ContentPage
{
    double number1 = 0;
    double number2 = 0;
    string operatorSymbol = "";
    bool isOperationClicked = false;

    public CalculatorPage()
    {
        InitializeComponent(); 
    }

    private void NumClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;

        if (cScreen.Text == "0" || isOperationClicked)
        {
            cScreen.Text = "";
            isOperationClicked = false;
        }

        cScreen.Text += btn.Text;
    }

    private void OperatorClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        number1 = double.Parse(cScreen.Text);
        number1 = double.Parse(cScreen.Text);
        number1 = double.Parse(cScreen.Text);
        operatorSymbol = btn.Text;
        lblHistory.Text = $"{number1} {operatorSymbol}";
        isOperationClicked = true;
    }

    private void EqualClicked(object sender, EventArgs e)
    {
        number2 = double.Parse(cScreen.Text);
        double result = 0;

        switch (operatorSymbol)
        {
            case "+":
                result = number1 + number2; break;
            case "-":
                result = number1 - number2; break;
            case "×":
                result =number1 * number2; break;
            case "÷":
                result =number1 / number2; break;
        }

        lblHistory.Text = $"{number1} {operatorSymbol} {number2} =";
        cScreen.Text = result.ToString();
        number1 = result;
        isOperationClicked = true;
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = "0";
        lblHistory.Text = "";
        number1 = 0;
        number2 = 0;
        operatorSymbol = "";
        isOperationClicked = false;
    }

    private void Square_Clicked(object sender, EventArgs e)
    {
        number1 = double.Parse(cScreen.Text);
        var result = number1 * number1;
        lblHistory.Text = $"{number1}² =";
        cScreen.Text = result.ToString();
        isOperationClicked = true;
    }

    private void BackSpaceClicked(object sender, EventArgs e)
    {
        if (cScreen.Text.Length > 1)
            cScreen.Text = cScreen.Text[..^1];
        else
            cScreen.Text = "0";
    }
    //trigonometrik komutlar 
    private void ScientificOperatorClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        double num = double.Parse(cScreen.Text);
        double result = 0;

        switch (btn.Text)
        {
            case "sin": result = Math.Sin(num * Math.PI / 180); break;
            case "cos": result = Math.Cos(num * Math.PI / 180); break;
            case "tan": result = Math.Tan(num * Math.PI / 180); break;
            case "ln": result = Math.Log(num); break;
            case "log": result = Math.Log10(num); break;
            case "e^x": result = Math.Exp(num); break;
        }

        lblHistory.Text = $"{btn.Text}({num}) =";
        cScreen.Text = result.ToString();
        isOperationClicked = true;
    }
    //pi say?s?n? g?steren komut 
    private void PiClicked(object sender, EventArgs e)
    {
        cScreen.Text = Math.PI.ToString();
        isOperationClicked = true;
    }
    //k?k alma komutu 
    private void SqrtClicked(object sender, EventArgs e)
    {
        double num = double.Parse(cScreen.Text);
        double result = Math.Sqrt(num);
        lblHistory.Text = $"?({num}) =";
        cScreen.Text = result.ToString();
        isOperationClicked = true;
    }
    //yüzde alma komutu 
    private void PercentClicked(object sender, EventArgs e)
    {
        double num = double.Parse(cScreen.Text);
        double result = num / 100;
        lblHistory.Text = $"{num}% =";
        cScreen.Text = result.ToString();
        isOperationClicked = true;
    }

   //1/x in komutu
    private void InverseClicked(object sender, EventArgs e)
    {
        double num = double.Parse(cScreen.Text);
        if (num == 0)
        {
            cScreen.Text = "Hata"; 
            return;
        }
        double result = 1 / num;
        lblHistory.Text = $"1/({num}) =";
        cScreen.Text = result.ToString();
        isOperationClicked = true;
    }





}
