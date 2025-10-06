using System.Globalization;
namespace KyKylator
{
    public partial class MainPage : ContentPage
    {
        private double _firstNumber = 0;
        private double _secondNumber = 0;
        private string _currentOperator = "";
        private bool _isOperatorClicked = false;
        private bool _isCalculationDone = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var number = button.Text;

            if (DisplayLabel.Text == "0" || _isOperatorClicked || _isCalculationDone)
            {
                DisplayLabel.Text = number;
                _isOperatorClicked = false;
                _isCalculationDone = false;
            }
            else
            {
                DisplayLabel.Text += number;
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (!_isOperatorClicked)
            {
                _firstNumber = double.Parse(DisplayLabel.Text);
            }

            _currentOperator = button.Text;
            _isOperatorClicked = true;
            _isCalculationDone = false;
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentOperator) && !_isCalculationDone)
            {
                _secondNumber = double.Parse(DisplayLabel.Text);
                var result = Calculate(_firstNumber, _secondNumber, _currentOperator);

                DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                _firstNumber = result;
                _isCalculationDone = true;
            }
        }

        private double Calculate(double first, double second, string operation)
        {
            return operation switch
            {
                "+" => first + second,
                "-" => first - second,
                "×" => first * second,
                "÷" => second != 0 ? first / second : throw new DivideByZeroException("Нельзя делить на ноль"),
                _ => throw new InvalidOperationException("Неизвестная операция")
            };
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            DisplayLabel.Text = "0";
            _firstNumber = 0;
            _secondNumber = 0;
            _currentOperator = "";
            _isOperatorClicked = false;
            _isCalculationDone = false;
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!DisplayLabel.Text.Contains('.'))
            {
                DisplayLabel.Text += ".";
            }
        }

        private void OnSignClicked(object sender, EventArgs e)
        {
            if (DisplayLabel.Text != "0")
            {
                var currentValue = double.Parse(DisplayLabel.Text);
                DisplayLabel.Text = (-currentValue).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void OnPercentageClicked(object sender, EventArgs e)
        {
            var currentValue = double.Parse(DisplayLabel.Text);
            DisplayLabel.Text = (currentValue / 100).ToString(CultureInfo.InvariantCulture);
        }
        private bool IsNullOrEmpty()
    }

}
