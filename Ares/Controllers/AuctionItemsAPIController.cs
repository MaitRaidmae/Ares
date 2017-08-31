using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ares.Models;
using Ares.Data;

namespace Ares.Controllers
{
    [Produces("application/json")]
    [Route("api/AuctionItemsAPI")]
    public class AuctionItemsAPIController : Controller
    {
        private readonly AresContext _context;

        public AuctionItemsAPIController(AresContext context)
        {
            _context = context;
        }
        // GET: api/AuctionItemsAPI
        [HttpGet]
        public IEnumerable<AuctionItem> Get()
        {
            var items = _context.AuctionItem.Where(ai => ai.ItemState == AuctionItemState.Active && ai.AuctionEndTime >= DateTime.Now);
            return items;
        }
      
    }
}
