using Ardalis.Result;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using refreshtoken = E_Commerce.Domain.Model.Token.RefreshToken;
namespace E_Commerce.Application.RefreshToken.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommandHandler : ICommandHandler<UpdateRefreshTokenCommand, refreshtoken>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<refreshtoken>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try {
                var refreshToken = await _unitOfWork.RefreshTokenRepository.GetById(request.RefreshTokenId);
                refreshToken.Update(request.userId,request.IssuedUtc,request.ExpiresUtc);
                var refreshTokenUpdated = await _unitOfWork.RefreshTokenRepository.Update(refreshToken);

                await _unitOfWork.save();

                return Result.Success(refreshToken);
            }catch (Exception ex) 
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
