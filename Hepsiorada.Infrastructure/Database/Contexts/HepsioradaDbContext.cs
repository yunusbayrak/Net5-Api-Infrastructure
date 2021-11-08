using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.ProductMeta;
using Hepsiorada.Domain.Users;
using Hepsiorada.Domain.Users.Lists;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Database.Contexts
{
    public class HepsiOradaDbContext : DbContext
    {
        public HepsiOradaDbContext(DbContextOptions<HepsiOradaDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserList> UserLists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
