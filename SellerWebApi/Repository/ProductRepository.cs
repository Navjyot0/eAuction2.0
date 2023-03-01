using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SellerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //Product specific code here
        public ProductRepository(IMongoCollection<Product> _dbContext) : base(_dbContext)
        {

        }
    }
}
