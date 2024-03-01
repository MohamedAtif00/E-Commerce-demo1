using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Category
{
    public class Visitor : Entity<VisitorId>
    {
        public ApplicationUserId UserId { get;private set; }
        public int visitCount { get;private set; }

        protected Visitor(VisitorId id,ApplicationUserId userId) : base(id)
        {
            this.UserId = userId;
        }

        public static Visitor Create(ApplicationUserId userId)
        {
            return new(VisitorId.CreateUnique(),userId);
        }

        public void AddVisitingCount()=> visitCount++;


        
    }
}
