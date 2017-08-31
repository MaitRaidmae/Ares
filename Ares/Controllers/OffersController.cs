using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ares.Models;
using Ares.Data;
using System;

namespace Ares.Controllers
{
    public class OffersController : Controller
    {
        private readonly AresContext _context;

        public OffersController(AresContext context)
        {
            _context = context;
        }

        // GET: Offers
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["BuyerNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewData["TimeSortParm"] = sortOrder == "Time" ? "time_desc" : "Time";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewData["AuctionItemSortParm"] = sortOrder == "AuctionItem" ? "auctionItem_desc" : "AuctionItem";

            var offers = from o in _context.Offer
                               select o;

            switch (sortOrder)
            {
                case "Name":
                    offers = offers.OrderBy(o => o.BuyerName);
                    break;
                case "name_desc":
                    offers = offers.OrderByDescending(o => o.BuyerName);
                    break;
                case "Time":
                    offers = offers.OrderBy(o => o.OfferTime);
                    break;
                case "time_desc":
                    offers = offers.OrderByDescending(o => o.OfferTime);
                    break;
                case "Amount":
                    offers = offers.OrderBy(o => o.OfferAmount);
                    break;
                case "amount_desc":
                    offers = offers.OrderByDescending(o => o.OfferAmount);
                    break;
                case "AuctionItem":
                    offers = offers.OrderBy(o => o.AuctionItem);
                    break;
                case "auctionItem_desc":
                    offers = offers.OrderByDescending(o => o.AuctionItem);
                    break;
            }

            List<Offer> sortedOffers = await offers.AsNoTracking().ToListAsync();
            foreach (Offer offer in sortedOffers)
            {
                offer.AuctionItem = _context.AuctionItem.Single(ai => ai.ID == offer.AuctionItemID);
            }

            return View(sortedOffers);

        }


        public async Task<IActionResult> ItemOffers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int itemID = id.Value;
            List<Offer> itemOffers = new List<Offer>(); 
            List<Offer> offers = await _context.Offer.ToListAsync();
            foreach (Offer offer in offers)
            {
                if (offer.AuctionItemID == itemID)
                {
                    itemOffers.Add(offer);
                }
            }

            return View(itemOffers);
        }
            

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            offer.AuctionItem = _context.AuctionItem.Single(ai => ai.ID == offer.AuctionItemID);

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BuyerName,OfferTime,Price")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offer.SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BuyerName,OfferTime,Price")] Offer offer)
        {
            if (id != offer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offer.SingleOrDefaultAsync(m => m.ID == id);
            _context.Offer.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.ID == id);
        }
    }
}
