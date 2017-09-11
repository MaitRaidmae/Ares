using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ares.Models;
using Ares.Data;
using Microsoft.EntityFrameworkCore;

namespace Ares.Controllers
{
    public class AuctionItemDetailController : Controller
    {
        private readonly AresContext _context;

        public AuctionItemDetailController(AresContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            var auctionItemDetail = new AuctionItemDetail();
            if (id == null)
            {
                return NotFound();
            }

            auctionItemDetail.Item = await _context.AuctionItem.SingleOrDefaultAsync(m => m.ID == id);

            if (auctionItemDetail.Item == null)
            {
                return NotFound();
            }

            string query = "SELECT * FROM Offer WHERE AuctionItemID = {0}";

            auctionItemDetail.Offers = await _context.Offer.FromSql(query, auctionItemDetail.Item.ID).AsNoTracking().ToListAsync();
                
            if (auctionItemDetail.Offers == null)
            {
                auctionItemDetail.Offers = new List<Offer>();
            }

            return View(auctionItemDetail);
        }

    }
}