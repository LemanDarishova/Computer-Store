using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class OrderDetailsViewModel
    {
        [Required]
        public Order Order { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
