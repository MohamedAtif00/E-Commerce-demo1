using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RefreshToken.DeleteRefreshToken
{
    public class DeleteRefreshTokenCommandHandler : ICommandHandler<DeleteRefreshTokenCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try {
                var refreshToken = await _unitOfWork.RefreshTokenRepository.GetById(request.RefreshTokenId);

                if (refreshToken == null) return Result.NotFound("this refreshToken is not exist");

                await _unitOfWork.RefreshTokenRepository.Delete(refreshToken);
                await _unitOfWork.save();

                refreshToken = await _unitOfWork.RefreshTokenRepository.GetById(request.RefreshTokenId);

                if (refreshToken != null) return Result.Error("refreshToken didn't be removed");
                return Result.Success(true);

            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
