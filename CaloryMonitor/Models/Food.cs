using System.ComponentModel.DataAnnotations;

public enum FoodStatus
{
    Vegan,
    NonVegan,
    Synthetic
}

public class Food
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Range(0, 1000)]
    public double CaloriesPer100g { get; set; }

    [Range(0, 100)]
    public double ProteinsPer100g { get; set; }

    [Range(0, 100)]
    public double CarbsPer100g { get; set; }

    [Range(0, 100)]
    public double FatsPer100g { get; set; }

    [Required]
    public FoodStatus Status { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
