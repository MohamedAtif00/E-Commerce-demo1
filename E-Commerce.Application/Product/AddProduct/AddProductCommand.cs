﻿using E_Commerce.Application.Common;
using E_Commerce.Domain.Model.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Application.Product.AddProduct
{
    public record AddProductCommand : ICommand<Domain.Model.Product.Product>
    {
        public AddProductCommand( Guid categoryId, string name, string description, decimal price)
        {
            CategoryId = categoryId;
            this.name = name;
            this.description = description;
            this.price = price;
        }
        public Guid CategoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }


    }
    
    
}
