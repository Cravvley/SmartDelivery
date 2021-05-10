using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using System.Collections.Generic;

namespace SmartDelivery.WEB.Models
{
    public class UserListViewModel
    {
        public IList<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
