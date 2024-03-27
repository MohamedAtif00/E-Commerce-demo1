using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.Token;
using E_Commerce.Domain.Model.User;

namespace E_Commerce.Application.RefreshToken.UpdateRefreshToken
{
    public record UpdateRefreshTokenCommand(RefreshTokenId RefreshTokenId,UserId userId, DateTime IssuedUtc, DateTime ExpiresUtc) :ICommand<Domain.Model.Token.RefreshToken>;
    
    
}
