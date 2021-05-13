
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartDelivery.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal TotalGrossPrice { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}