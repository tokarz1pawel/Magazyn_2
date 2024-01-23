using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazyn.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        // Dodaj UserId jako pole
        [Column(TypeName = "nvarchar(450)")] // Typ danych odpowiadający kluczowi głównemu w tabeli AspNetUsers
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

        // Dodaj relację jeden-do-wielu z OrderItems
        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
