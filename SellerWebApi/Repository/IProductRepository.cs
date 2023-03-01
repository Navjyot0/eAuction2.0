using SellerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        //Product specific code here
    }
}
