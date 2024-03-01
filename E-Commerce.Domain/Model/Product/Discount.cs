using E_Commerce.Domain.Common.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.Product
{
    public class Discount : ValueObject
    {
        public int DiscountAmount { get;private set; }
        public DateTime? DiscountStart { get;private set; }
        public DateTime? DiscountEnd { get;private set; }

        private Discount(int DiscountAmount,DateTime? DiscountStart ,DateTime? DiscountEnd)
        {
            this.DiscountAmount = DiscountAmount;
            this.DiscountStart = DiscountStart;
            this.DiscountEnd = DiscountEnd;
        }

        public bool IsOnDiscount()
        {
            return DiscountAmount > 0 ? DiscountEnd == null || DiscountEnd > DateTime.UtcNow : false;       
        }

        public static Discount Create(int DiscountAmount,DateTime? DiscountStart = null,DateTime? DiscountEnd = null)
        {
            return new(DiscountAmount,DiscountStart,DiscountEnd);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return DiscountAmount;
            if (DiscountStart != null && DiscountEnd != null)
            {
                yield return DiscountStart;
                yield return DiscountEnd;               
            }
        }
    }
}
