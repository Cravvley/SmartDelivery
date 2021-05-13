using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartDelivery.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartDelivery.Data
{
    public static class DbInitializer
    {
        const string Admin = "Admin";
        const string RestaurantWorker = "RestaurantWorker";
        const string DefaultUser = "DefaultUser";

        public static void Seed(AppDbContext appDbContext)
        {
            UpdateMigration(appDbContext);
     
        }

        private static void UpdateMigration(AppDbContext appDbContext)
        {
            try
            {
                if (appDbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    appDbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
