using System.Windows.Media;

namespace WpfApp1
{
    public class BMIResult
    {
        public double Score { get; }
        public string Description { get; }
        public Brush Color { get; }

        private BMIResult(double score, string description, Brush color)
        { 
            Score = score;
            Color = color;
            Description = description;
        }

        public static BMIResult Create(double score, string description, Brush color)
        {
            return new(score, description, color);
        }
    }
}
