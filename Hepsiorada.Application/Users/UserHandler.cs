using Hepsiorada.Application.Securiy;
using Hepsiorada.Application.Users.Commands;
using Hepsiorada.Application.Users.Queries;
using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Users;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Users
{
    public class UserHandler : IRequestHandler<CreateUserCommand, Unit>,
         IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>, IRequestHandler<UpdateUserCommand, Unit>
        , IRequestHandler<DeleteUserCommand, Unit>, IRequestHandler<GetUserByEmailAndPasswordQuery, UserDto>

    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = request.Adapt<User>();
            userEntity.Password = EncryptionHelper.EncryptString(userEntity.Password, EncryptionHelper.SymmetricKey);

            //Save entities to db
            await _unitOfWork.UserRepository.Add(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users.Adapt<IEnumerable<UserDto>>();
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            return user.Adapt<UserDto>();
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Update(request.Adapt<User>());
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<UserDto> Handle(GetUserByEmailAndPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEMail(request.Email);
            if (user == null)
            {
                throw new ApplicationException("Email or password is wrong!");
            }
            var decryptedPass = EncryptionHelper.DecryptString(user.Password, EncryptionHelper.SymmetricKey);
            if (decryptedPass != request.Password)
            {
                throw new ApplicationException("Email or password is wrong!");
            }
            return user.Adapt<UserDto>();
        }
    }
}
