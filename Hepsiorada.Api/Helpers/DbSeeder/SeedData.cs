using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.ProductMeta;
using Hepsiorada.Infrastructure.Database.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Helpers.DbSeeder
{
    public static class SeedData
    {

        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetService<HepsiOradaDbContext>();
            db.Database.Migrate();
            if (!db.Categories.Any())
            {
                var ct = new List<Category>()
                {
                    new Category(){Name="Dizüstü Bilgisayar",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Tablet",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Masaüstü Bilgisayar",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Oyuncu Özel",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Veri Depolama",CreatedAt=DateTime.Now,IsDeleted=false}
                };
                foreach (var item in ct)
                {
                    var rnd = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        item.Products.Add(new Product()
                        {
                            Name = $"Product {i}",
                            Brand = $"Brand {rnd.Next(0, 5)}",
                            CreatedAt = DateTime.Now,
                            Price = rnd.Next(80, 150),
                            DiscountedPrice = rnd.Next(50, 80),
                            IsDeleted = false,
                            Stock = rnd.Next(50, 100)
                        });
                    }
                    db.Categories.Add(item);
                }
                db.SaveChanges();
            }
        }
    }
}
