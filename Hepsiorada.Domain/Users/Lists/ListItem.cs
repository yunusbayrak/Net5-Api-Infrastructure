using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using System;

namespace Hepsiorada.Domain.Users.Lists
{
    public class ListItem : IEntityHasId
    {
        public Guid UserListId { get; set; }
        public Guid ProductId { get; set; }
        public int Sort { get; set; }
        public virtual Product Product { get; set; }
        public virtual UserList UserList { get; set; }
    }
}
