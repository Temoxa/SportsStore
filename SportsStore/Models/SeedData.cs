using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        //    if (!context.Products.Any())
        //    {
        //        context.Products.AddRange(
        //            new Product { Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275 },
        //            new Product { Name = "Lifejacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48.95m },
        //            new Product { Name = "Soccer Ball", Description = "For FIFA", Category = "Soccer", Price = 19.50m },
        //            new Product { Name = "Corner Flags", Description = "Give youer playing field", Category = "Soccer", Price = 34.95m },
        //            new Product { Name = "Stadium", Description = "Flat-packed", Category = "Soccer", Price = 79500 },
        //            new Product { Name = "Thinking Cap", Description = "Improve brain", Category = "Chess", Price = 16 },
        //            new Product { Name = "Unsteady Chair", Description = "Secretly give your", Category = "Chess", Price = 29.95m },
        //            new Product { Name = "Human Cahess Board", Description = "A fun game", Category = "Chess", Price = 75 },
        //            new Product { Name = "Bling-Bling King", Description = "Gold-plated", Category = "Chess", Price = 1200 }
        //            );
        //        context.SaveChanges();
        //    }
        //}

        public static void Initialize(ApplicationDbContext serviceProvider)
        {
            ApplicationDbContext context = serviceProvider;

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275 },
                    new Product { Name = "Lifejacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48.95m },
                    new Product { Name = "Soccer Ball", Description = "For FIFA", Category = "Soccer", Price = 19.50m },
                    new Product { Name = "Corner Flags", Description = "Give youer playing field", Category = "Soccer", Price = 34.95m },
                    new Product { Name = "Stadium", Description = "Flat-packed", Category = "Soccer", Price = 79500 },
                    new Product { Name = "Thinking Cap", Description = "Improve brain", Category = "Chess", Price = 16 },
                    new Product { Name = "Unsteady Chair", Description = "Secretly give your", Category = "Chess", Price = 29.95m },
                    new Product { Name = "Human Cahess Board", Description = "A fun game", Category = "Chess", Price = 75 },
                    new Product { Name = "Bling-Bling King", Description = "Gold-plated", Category = "Chess", Price = 1200 }
                    );
                context.SaveChanges();
            }
        }
    }
}
