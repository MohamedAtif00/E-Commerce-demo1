using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RefreshToken.GetRefreshTokenByUserId
{
    public record GetRefreshTokenByUserIdQuery(UserId UserId):IQuery<Domain.Model.Token.RefreshToken>;
    
    
}
