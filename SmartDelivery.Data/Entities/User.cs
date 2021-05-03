using Microsoft.AspNetCore.Identity;
using System;

namespace SmartDelivery.Data.Entities
{
    public class User :IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModificationAt { get; set; }
    }
}
