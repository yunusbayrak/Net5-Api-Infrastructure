using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products.ProductMeta;
using Hepsiorada.Domain.Users.Lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hepsiorada.Domain.Products
{
    public class Product : IAuditibleEntitiy
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        [Required]
        public string Brand { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ListItem> UserListedProducts { get; set; }
    }
}
