using E_Commerce.Domain.Common.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Product.Rule
{
    internal class PriceNotEqualOrBelowZeroRule : IBusinessRule
    {
        public decimal price { get; private set; }

        public PriceNotEqualOrBelowZeroRule(decimal price)
        {
            this.price = price;
        }


        public bool IsBroken()
        {
            return this.price > 0; 
        }

        public string Message => "Product price should be higher than zero";
    }
}
