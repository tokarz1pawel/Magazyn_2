using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Magazyn.Models
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Orders Orders { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public double Price { get; set; }
    }
}