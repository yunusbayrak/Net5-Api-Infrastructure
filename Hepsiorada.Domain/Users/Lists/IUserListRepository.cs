

using Hepsiorada.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Users.Lists
{
    public interface IUserListRepository : IRepository<UserList>
    {
        Task AddToList(ListItem entity);
        Task AddToList(IEnumerable<ListItem> list);
        Task RemoveFromList(ListItem entity);
        /// <summary>
        /// This will remove the products from list by id that passed as parameter list
        /// </summary>
        /// <param name="ids">the ids of the products in the list</param>
        /// <returns></returns>
        Task RemoveFromList(IEnumerable<Guid> ids);
        Task RemoveFromList(Guid id);
        Task<IEnumerable<ListItem>> GetListItemsByListId(Guid id);
        Task<IEnumerable<ListItemCountDto>> GetMostListedProducts(int count);
        Task<IEnumerable<UserListItemCountDto>> GetUsersMostListedProducts(int count);
    }
}
