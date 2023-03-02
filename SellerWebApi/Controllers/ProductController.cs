﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                this.db.product.Add(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}