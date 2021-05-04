using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using System.Collections.Generic;

namespace SmartDelivery.WEB.Models
{
    public class DishViewModel
    {
        public IList<Dish> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
