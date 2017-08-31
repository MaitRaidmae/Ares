using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ares.Data;
using Ares.Models;

namespace Ares.Controllers
{
    [Produces("application/json")]
    [Route("api/OffersAPI")]
    public class OffersAPIController : Controller
    {
        private readonly AresContext _context;

        public OffersAPIController(AresContext context)
        {
            _context = context;
        }

        // POST: api/OffersAPI
        [HttpPost]
        public async Task<IActionResult> PostOffer([FromBody] Offer offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuctionItem auctionItem = _context.AuctionItem.First(ai => ai.ID == offer.AuctionItemID);
            if (auctionItem.ItemState != AuctionItemState.Active)
            {
                return StatusCode(902);
            } else if (auctionItem.AuctionEndTime < offer.OfferTime)
            {
                return StatusCode(901);
            } else
            {
                _context.Offer.Add(offer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOffer", new { id = offer.ID }, offer);
            }            
        }
      
    }
}