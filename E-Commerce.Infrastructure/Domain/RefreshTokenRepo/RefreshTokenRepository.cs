using E_Commerce.Domain.Model.Token;
using E_Commerce.Domain.Model.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.RefreshTokenRepo
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken, RefreshTokenId> ,IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContextClass context) : base(context)
        {

        }

        public async Task<RefreshToken> GetRefreshTokenByUserId(UserId userId)
        {
            return await _context.Set<RefreshToken>().AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
