using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.RootCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Persistent.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IRootCategoryRepository RootCategoryRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> save();
    }
}
