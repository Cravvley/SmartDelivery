using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.DTOs;
using System.Collections.Generic;

namespace SmartDelivery.WEB.Models
{
    public class RestaurantListViewModel
    {
        public IList<RestaurantDto> Restaurants{ get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
