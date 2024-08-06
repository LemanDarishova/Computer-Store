using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int ComputerId { get; set; }
    [ForeignKey("ComputerId")]
    public Computer? Computer { get; set; }
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public DateTime Order_date { get; set; }

}
