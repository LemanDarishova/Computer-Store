using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.Models;

public class Computer
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string Brands { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int Stock { get; set; }
    public int CategorieId{ get; set; }
    [ForeignKey("CategorieId")]
    public Categorie Categorie { get; set; }
    public int SizesId{ get;set; }
    [ForeignKey("SizesId")]
    public Sizee Sizes { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
