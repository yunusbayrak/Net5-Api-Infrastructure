using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.UserLists.Commands
{
    public class DeleteListItemsCommand:IRequest
    {
        public Guid ListId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
        public DeleteListItemsCommand(Guid listId, Guid userId, IEnumerable<Guid> productIds)
        {
            ListId = listId;
            UserId = userId;
            ProductIds = productIds;
        }
    }
}
