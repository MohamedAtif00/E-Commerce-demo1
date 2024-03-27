using Ardalis.Result;
using E_Commerce.Application.Authentication;
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
    public class AddNewUserCommandHandler : ICommandHandler<AddNewUserCommand, JwtTokenDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public AddNewUserCommandHandler(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task<Result<JwtTokenDto>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var jwtToken = new JwtTokenDto();
                var newuser = await _authenticationService.Register(request.FirstName,request.LastName,request.Username,request.Email,request.Password,request.PhoneNumber,request.Role);

                if (newuser.Value.Id == Guid.Empty) return Result.Error("the id is null");
                var user = await _unitOfWork.UserRepository.Add(Domain.Model.User.User.Create(UserId.Create(newuser.Value.Id),request.FirstName,request.LastName,request.Username,request.Email));

                // Generate Claim idnetity
                var claimIdentity = await _authenticationService.GenerateClaimsIdentity(newuser);// used only for generateAccessToken
                // Generate access Token
                var accessTokens = await _authenticationService.GenerateAccessToken(claimIdentity);
                // Generate refreshToken
                var refreshToken = await _authenticationService.GenerateRefreshToken(newuser);

                jwtToken.JwtToken = accessTokens;
                jwtToken.RefreshToken = refreshToken;   


                if (user == null)
                {
                    return Result.Error("user didn't created");
                }


                await _unitOfWork.save();

                return Result.Success(jwtToken);

            }catch(Exception ex) 
            {
                //return Result.Error(ex.Message);
                throw ex;
            }

            
        }
    }
}
