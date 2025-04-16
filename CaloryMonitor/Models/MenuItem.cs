using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MenuItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MenuId { get; set; }

    [ForeignKey("MenuId")]
    public Menu Menu { get; set; }

    [Required]
    public int FoodId { get; set; }

    [ForeignKey("FoodId")]
    public Food Food { get; set; }

    [Range(1, 10000)]
    public double QuantityGrams { get; set; }

    [Required(ErrorMessage = "Моля, изберете време на деня.")]
    public string TimeOfTheDay { get; set; }
}
