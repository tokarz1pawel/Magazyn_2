using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazyn.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public double TotalPrice { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedAt { get; set; }
    }
}