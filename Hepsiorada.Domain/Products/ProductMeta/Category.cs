using Hepsiorada.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Products.ProductMeta
{
    public class Category : IAuditibleEntitiy
    {
        public Guid? ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
