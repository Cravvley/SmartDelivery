using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartDelivery.Data.Entities
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, StringLength(11)]
        public string Nip { get; set; }

        [Required]
        public DateTime OpenAt { get; set; }

        [Required]
        public DateTime CloseAt { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public virtual IList<User> Employees { get; set; }

        public virtual IList<Dish> Meals{ get; set; }
    }
}
