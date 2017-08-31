using Ares.Data;
using Ares.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ares.Controllers
{
    public class AuctionItemsController : Controller
    {
        private readonly AresContext _context;

        public AuctionItemsController(AresContext context)
        {
            _context = context;
        }

        // GET: AuctionItems
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "descr_desc" : "Description";
            ViewData["EndTimeSortParm"] = sortOrder == "EndTime" ? "endTime_desc" : "EndTime";
            ViewData["StateSortParm"] = sortOrder == "State" ? "state_desc" : "State";

            var auctionItems = from ai in _context.AuctionItem
                               select ai;

            switch (sortOrder)
            {
                case "Name":
                    auctionItems = auctionItems.OrderBy(ai => ai.Name);
                    break;
                case "name_desc":
                    auctionItems = auctionItems.OrderByDescending(ai => ai.Name);
                    break;
                case "Description":
                    auctionItems = auctionItems.OrderBy(ai => ai.Name);
                    break;
                case "descr_desc":
                    auctionItems = auctionItems.OrderByDescending(ai => ai.Description);
                    break;
                case "EndTime":
                    auctionItems = auctionItems.OrderBy(ai => ai.AuctionEndTime);
                    break;
                case "endTime_desc":
                    auctionItems = auctionItems.OrderByDescending(ai => ai.AuctionEndTime);
                    break;
                case "State":
                    auctionItems = auctionItems.OrderBy(ai => ai.ItemState);
                    break;
                case "state_desc":
                    auctionItems = auctionItems.OrderByDescending(ai => ai.ItemState);
                    break;
            }

            return View(await auctionItems.AsNoTracking().ToListAsync());
        }

        // GET: AuctionItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem
             
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            auctionItem.Offers = _context.Offer.Where(o => o.AuctionItemID == auctionItem.ID) as List <Offer>;

            return View(auctionItem);
        }

        // GET: AuctionItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuctionItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,AuctionEndTime,ItemState")] AuctionItem auctionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auctionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auctionItem);
        }

        // GET: AuctionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem.SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }
            return View(auctionItem);
        }

        // POST: AuctionItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,AuctionEndTime,ItemState")] AuctionItem auctionItem)
        {
            if (id != auctionItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auctionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionItemExists(auctionItem.ID))
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
            return View(auctionItem);
        }

        // GET: AuctionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            return View(auctionItem);
        }

        // POST: AuctionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auctionItem = await _context.AuctionItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.AuctionItem.Remove(auctionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AuctionItems/Delete/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            auctionItem.ItemState = AuctionItemState.Active;

            _context.AuctionItem.Update(auctionItem);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItem.Any(e => e.ID == id);
        }


    }
}
