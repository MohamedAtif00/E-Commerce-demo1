using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Token
{
    public class RefreshToken : AggregateRoot<RefreshTokenId>
    {
        public UserId UserId { get;private set; }
        public DateTime IssuedUtc { get; private set; }
        public DateTime ExpiresUtc { get; private set; }

        public RefreshToken(RefreshTokenId id):base(id)
        {
            
        }
        protected RefreshToken(RefreshTokenId id,UserId userId,DateTime IssuedUtc,DateTime ExpiresUtc) : base(id)
        {
            UserId = userId;
            this.IssuedUtc = IssuedUtc;
            this.ExpiresUtc = ExpiresUtc;
        }

        public static RefreshToken Create(UserId userId, DateTime IssuedUtc, DateTime ExpiresUtc)
        {
            return new(RefreshTokenId.CreateUnique(),userId,IssuedUtc,ExpiresUtc);
        }

        public void Update(UserId userId, DateTime IssuedUtc, DateTime ExpiresUtc)
        {
            this.UserId = userId;
            this.IssuedUtc = IssuedUtc;
            this.ExpiresUtc = ExpiresUtc;
        }

    }
}
