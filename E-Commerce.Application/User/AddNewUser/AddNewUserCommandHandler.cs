using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.User.AddNewUser
{
    public class AddNewUserCommandHandler : ICommandHandler<AddNewUserCommand, Domain.Model.User.User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNewUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.User.User>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                if (request.UserId == null) return Result.Error("the id is null");
                var user = await _unitOfWork.UserRepository.Add(Domain.Model.User.User.Create(request.UserId,request.FirstName,request.LastName,request.Username,request.Email));

                if (user == null)
                {
                    return Result.Error("user didn't created");
                }


                await _unitOfWork.save();

                return Result.Success(user);

            }catch(Exception ex) 
            {
                //return Result.Error(ex.Message);
                throw ex;
            }

            
        }
    }
}
