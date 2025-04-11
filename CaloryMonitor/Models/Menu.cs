using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Menu
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }  // FK към AspNetUsers

    [Required]
    public DateTime Date { get; set; }

    public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
}
