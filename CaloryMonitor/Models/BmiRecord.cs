using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BmiRecord
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Range(1, 300)]
    public double WeightKg { get; set; }

    [Range(0.5, 2.5)]
    public double HeightMeters { get; set; }

    [NotMapped]
    public double Bmi => WeightKg / (HeightMeters * HeightMeters);
}
    