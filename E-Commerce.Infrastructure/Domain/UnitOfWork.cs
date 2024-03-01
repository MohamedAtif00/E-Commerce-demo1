﻿using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.RootCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;

        public UnitOfWork(DbContextClass dbContext, IProductRepository productRepository, IRootCategoryRepository rootCategoryRepository, ICategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            RootCategoryRepository = rootCategoryRepository;
            CategoryRepository = categoryRepository;
        }

        public IProductRepository ProductRepository { get; }

        public IRootCategoryRepository RootCategoryRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<int> save()
        {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}