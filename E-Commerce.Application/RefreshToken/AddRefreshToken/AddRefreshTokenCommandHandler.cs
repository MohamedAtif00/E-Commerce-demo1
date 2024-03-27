using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using  Refreshtoken = E_Commerce.Domain.Model.Token.RefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RefreshToken.AddRefreshToken
{
    public class AddRefreshTokenCommandHandler : ICommandHandler<AddRefreshTokenCommand, Refreshtoken>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Domain.Model.Token.RefreshToken>> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try {
                var refreshToken = await _unitOfWork.RefreshTokenRepository.Add(Refreshtoken.Create(request.UserId,request.IssuedUtc,request.ExpiresUtc));

                await _unitOfWork.save();

                return Result.Success(refreshToken);
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
