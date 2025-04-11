using System.ComponentModel.DataAnnotations;

namespace CaloryMonitor.ViewModels
{
    public class BmiViewModel
    {
        [Required]
        [Range(0.5, 2.5)]
        public double HeightMeters { get; set; }

        [Required]
        [Range(1, 300)]
        public double WeightKg { get; set; }

        public double? BmiResult { get; set; }
        public string? Interpretation { get; set; }
    }

}
