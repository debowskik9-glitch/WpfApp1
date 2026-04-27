using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public readonly BmiRepository _repository = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_repository.Initialized)
            {
                _repository.Init();
            }

            if (double.TryParse(weightBox.Text, out double w) &&
                double.TryParse(heightBox.Text, out double h) && w > 0 && h > 0)


            {
                var calculator = new BMICalculator();
                var result = calculator.Calculate(w, h);

                resultBMI.Text = result.Score.ToString("0.00");
                category.Text = result.Description;
                category.Background = result.Color;


                if (_repository.TrySave(w, h, result))
                {
                    MessageBox.Show("Dane zostały zapisane.");
                }
                else
                {
                    MessageBox.Show("Wystąpił problem z zapisem danych.");
                }
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            weightBox.Clear();
            heightBox.Clear();
            resultBMI.Clear();
            category.Clear();
            category.Background = Brushes.Transparent;
        }
        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Name == "heightBox" && string.IsNullOrEmpty(tb.Text))
            {

                e.Handled = !new Regex("[1-2]").IsMatch(e.Text);
                return;
            }
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}