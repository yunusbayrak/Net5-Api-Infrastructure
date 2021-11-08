using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Users.Lists
{
    public class UserListItemCountDto : ListItemCountDto
    {
        public Guid UserId { get; set; }
    }
}
