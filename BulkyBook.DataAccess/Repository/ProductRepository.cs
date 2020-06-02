﻿using BulkyBook.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);

            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
            }

            objFromDb.Author = product.Author;
            objFromDb.Category = product.Category;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.CoverType = product.CoverType;
            objFromDb.CoverTypeId = product.CoverTypeId;
            objFromDb.Description = product.Description;
            objFromDb.ISBN = product.ISBN;
            objFromDb.ListPrice = product.ListPrice;
            objFromDb.Price = product.Price;
            objFromDb.Price50 = product.Price50;
            objFromDb.Price100 = product.Price100;
        }
    }
}
