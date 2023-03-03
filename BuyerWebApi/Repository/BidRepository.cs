using BuyerWebApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        //Product specific code here
        public BidRepository(IMongoCollection<Bid> _dbContext) : base(_dbContext)
        {

        }
    }
}
