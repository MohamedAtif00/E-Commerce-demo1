using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.RootCategory;
using E_Commerce.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Category
{
    public sealed class Category : AggregateRoot<CategoryId>
    {
        private readonly List<Category> _categories = new();
        private readonly List<Visitor> _visitor =  new();
        // ------------------------------

        public IReadOnlyCollection<Category> ChildsCategories => _categories;
        public IReadOnlyCollection<Visitor> Visitors => _visitor;
        public RootCategoryId? RootCategoryId { get;private set; }
        public CategoryId? ParentCategoryId { get;private set; }
        public string Name {  get;private set; }

        public Category(CategoryId id):base(id)
        {
            
        }
        private Category(CategoryId id,string name,RootCategoryId? rootCategoryId,CategoryId? ParentCategoryId) : base(id)
        {
            Name = name;
            this.ParentCategoryId = ParentCategoryId;
            this.RootCategoryId = rootCategoryId;
        }

        public static Category CreateRootCategory(string name,RootCategoryId rootCategoryId)
        {
            return new(CategoryId.CreateUnique(), name,rootCategoryId,null);
        }

        public static Category CreateChildCategory(string name,CategoryId ParentCategoryId)
        {
            return new(CategoryId.CreateUnique(), name,null, ParentCategoryId);
        }

        public void Update(string name)
        {
            this.Name = name;
        }

        public Product.Product AddProduct(string name,string description,decimal price)
        {
            return Product.Product.Create(Id.value,name,description,price);
        }

        public void AddNewVisitUser(UserId userId)
        {
            _visitor.Add(Visitor.Create(userId,Id));
        }
    }
}
