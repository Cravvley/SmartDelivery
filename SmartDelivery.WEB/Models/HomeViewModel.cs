using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using System.Collections.Generic;


namespace SmartDelivery.WEB.Models
{
    public class HomeViewModel
    {
        public IList<Restaurant> Restaurants{ get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
