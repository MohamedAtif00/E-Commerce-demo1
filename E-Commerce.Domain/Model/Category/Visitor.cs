using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Category
{
    public class Visitor : Entity<UserId>
    {
        public CategoryId categoryId { get;private set; }
        public int visitCount { get;private set; }

        private Visitor() : base(UserId.CreateUnique())
        { 
        }
        private Visitor(UserId userId,CategoryId categoryId) : base(userId)
        {
            this.categoryId = categoryId;
            visitCount = 1;
        }

        public static Visitor Create(UserId userId,CategoryId categoryId)
        {
            return new(userId,categoryId);
        }

        public void AddVisitingCount()=> visitCount++;


        
    }
}
