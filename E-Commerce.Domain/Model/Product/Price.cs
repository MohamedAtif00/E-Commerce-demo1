using E_Commerce.Domain.Common.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Product
{
    public class Price : ValueObject
    {
        public decimal Value { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal value,string currency)
        {
            Value = value;
            Currency = currency;
        }

        public static Price Create(decimal value,string currency)
        {
            return new(value,currency);
        }




        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
