using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models;

public class Sizee
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();




}
