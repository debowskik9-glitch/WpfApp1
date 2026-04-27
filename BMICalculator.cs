
using System.Windows.Media;
using WpfApp1.Abstracts;

namespace WpfApp1
{
    public class BMICalculator : IBmiCalculator
    {
        public BMIResult Calculate(double weight, double heightCm)
        {
            double bmi = CalculateBmi(weight, heightCm);
            
            switch (bmi)
            {
                case < 18.5:
                    return BMIResult.Create(bmi, "Zjedz coś", Brushes.Orange);

                case < 24.99:
                    return BMIResult.Create(bmi, "Waga prawidłowa", Brushes.Green);

                case < 29.99:
                    return BMIResult.Create(bmi, "Idź na siłownie", Brushes.Orange);

                default:
                    return BMIResult.Create(bmi, "wstydź się", Brushes.Red);
            }
        }

        private static double CalculateBmi(double weight, double heightCm) 
        {
            var heightM = CalculteHigh(heightCm);
            return weight / (heightM * heightM);
        }
        private static double CalculteHigh(double heightCm)
        {
            return heightCm / 100;
        }
    }
}
