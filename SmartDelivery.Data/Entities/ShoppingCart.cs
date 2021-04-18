using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartDelivery.Data.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int DishId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("DishId")]
        public virtual Dish Dish { get; set; }
    }
}
