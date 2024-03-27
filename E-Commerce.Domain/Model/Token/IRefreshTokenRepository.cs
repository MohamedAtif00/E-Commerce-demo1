using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Token
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken, RefreshTokenId>
    {
        Task<RefreshToken> GetRefreshTokenByUserId(UserId userId);
    }
}
