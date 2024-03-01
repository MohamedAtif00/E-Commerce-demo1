using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.RootCategory
{
    public sealed class RootCategory : AggregateRoot<RootCategoryId>
    {


        public string Name { get; private set; }
        public bool IsActive { get; private set; } = true;
        public RootCategory(RootCategoryId id,string name,bool isActive = true) : base(id)
        {
            Name = name;
            IsActive = isActive;
        }

        public static RootCategory Create(string name,bool isActive = true)
        {
            return new(RootCategoryId.CreateUnique(),name,isActive);
        }

        public void Update(string name,bool isActive = true)
        {
            this.Name = name;
            IsActive = isActive;
        }

    }
}
