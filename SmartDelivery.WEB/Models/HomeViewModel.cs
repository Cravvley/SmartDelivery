using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.DTOs;
using System.Collections.Generic;


namespace SmartDelivery.WEB.Models
{
    public class HomeViewModel
    {
        public IList<RestaurantDto> Restaurants{ get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
