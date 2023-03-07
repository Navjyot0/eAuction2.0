using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebApi.Models;
using SellerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SellerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork db;
        public ProductController(IUnitOfWork _db)
        {
            this.db = _db;
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                this.db.product.Add(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet]
        [Route("GetProductList")]
        public ActionResult<IEnumerable<Product>> GetProductList()
        {
            return Ok(this.db.product.GetAll());
        }

        [HttpGet]
        [Route("GetProductDetails")]
        public ActionResult<Product> GetProductDetails(string productId)
        {
            Product product = this.db.product.GetProduct(productId);
            ProductDetails productDetails = new ProductDetails()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription,
                DetailedDescription = product.DetailedDescription,
                Category = product.Category,
                StartingPrice = product.StartingPrice,
                BidEndDate = product.BidEndDate,
                SellerDetails = this.db.user.GetUser(product.SellerEmailId)
            };

            return Ok(productDetails);
        }

        [HttpGet]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct(string productId)
        {
            this.db.product.DeleteProduct(productId);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
