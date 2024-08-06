using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models;

public class Categorie
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
}
