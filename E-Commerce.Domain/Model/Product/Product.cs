using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Product
{
    public sealed class Product : AggregateRoot<ProductId>
    {

        public CategoryId CategoryId { get;private set; }
        public string ProductName { get; private set; }
        public string Description { get;private set; }
        public Price Price { get; private set; }
        public Discount Discount { get; private set; }

        public Product(ProductId id):base(id)
        {
            
        }
        private Product(ProductId id,string productName,string description,decimal price,CategoryId categoryId) : base(id)
        {
            this.CheckRule(new PriceNotEqualOrBelowZeroRule(price));

            ProductName = productName;
            Description = description;
            this.CategoryId = categoryId;
            AddPrice(price);
        }

        public static Product Create(Guid categoryId,string productName,string description,decimal price)
        {
            return new(ProductId.CreateUnique(),productName,description,price,CategoryId.Create(categoryId));
        }

        public void Update(string productName,string description,decimal price) 
        {
            this.ProductName = productName;
            this.Description = description;
            AddPrice(price);
        }

        public void AddPrice(decimal price)
        {
            Price = Price.Create(price,"USD");
        }

        public void AddDiscount(int value)
        {
            this.Discount = Discount.Create(value);
        }

        public void ChangeDiscount(int value)
        { 
            this.Discount = Discount.Create(value);
        }

        


    }
}
