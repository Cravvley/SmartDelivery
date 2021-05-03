using System.Collections.Generic;

namespace SmartDelivery.Infrastructure.Models
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<SubCategoryRequest> Subs { get; set; }
    }
}
