using BuyerWebApi.Models;
using BuyerWebApi.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuyerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        private readonly IUnitOfWork db;
        public BuyerController(IUnitOfWork _db)
        {
            this.db = _db;
        }

        [HttpPost]
        [Route("PlaceBid")]
        public ActionResult PlaceBid(Bid bid)
        {
            if (ModelState.IsValid)
            {
                var user = System.Security.Claims.ClaimTypes.NameIdentifier;
                var email = User.FindFirst("sub")?.Value;
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                this.db.bid.Add(bid);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
