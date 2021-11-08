using Hepsiorada.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hepsiorada.Domain.Users.Lists
{
    public class UserList : IAuditibleEntitiy
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ListItem> Items { get; set; }
    }
}
