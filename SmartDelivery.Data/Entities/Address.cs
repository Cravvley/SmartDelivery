using System.ComponentModel.DataAnnotations;

namespace SmartDelivery.Data.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

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
    }
}
