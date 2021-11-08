using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Users.Lists;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hepsiorada.Domain.Users
{
    public class User : IAuditibleEntitiy
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserList> Lists { get; set; }
    }
}
