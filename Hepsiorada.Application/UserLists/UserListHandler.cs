using Hepsiorada.Application.UserLists.Commands;
using Hepsiorada.Application.UserLists.Queries;
using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Users.Lists;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Application.UserLists
{
    public class UserListHandler : IRequestHandler<CreateUserListCommand, Unit>,
         IRequestHandler<GetUserListsQuery, IEnumerable<UserListDto>>, IRequestHandler<UpdateUserListCommand, Unit>
        , IRequestHandler<DeleteUserListCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserListHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<UserListDto>> Handle(GetUserListsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.UserListRepository.GetAll();
            return products.Adapt<IEnumerable<UserListDto>>();
        }

        public async Task<Unit> Handle(DeleteUserListCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserListRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(CreateUserListCommand request, CancellationToken cancellationToken)
        {
            var userListEntity = request.Adapt<UserList>();
            //Save entities to db
            await _unitOfWork.UserListRepository.Add(userListEntity);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserListCommand request, CancellationToken cancellationToken)
        {
            var userList = await _unitOfWork.UserListRepository.Get(x => x.Id == request.Id && x.UserId == request.UserId);
            if (userList.Count() == 0)
            {
                throw new ApplicationException("User List Not Found!");
            }
            var userListEntity = request.Adapt<UserList>();
            //Save entities to db
            await _unitOfWork.UserListRepository.Update(userListEntity);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
