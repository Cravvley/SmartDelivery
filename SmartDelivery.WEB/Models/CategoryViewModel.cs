using SmartDelivery.Data.Entities;
using System.Collections.Generic;

namespace SmartDelivery.WEB.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
