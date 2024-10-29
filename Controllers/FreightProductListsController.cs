using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    public class FreightProductListsController : Controller
    {
        private readonly AppDbContext _context;

        public FreightProductListsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FreightProductLists
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FreightProductLists.Include(f => f.FreightOutbound).Include(f => f.Inventory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: FreightProductLists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freightProductList = await _context.FreightProductLists
                .Include(f => f.FreightOutbound)
                .Include(f => f.Inventory)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (freightProductList == null)
            {
                return NotFound();
            }

            return View(freightProductList);
        }

        // GET: FreightProductLists/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.FreightOutbounds, "OutboundOrderId", "OutboundOrderId");
            ViewData["ProductId"] = new SelectList(_context.Inventories, "ProductId", "ProductId");
            return View();
        }

        // POST: FreightProductLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity")] FreightProductList freightProductList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(freightProductList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.FreightOutbounds, "OutboundOrderId", "OutboundOrderId", freightProductList.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Inventories, "ProductId", "ProductId", freightProductList.ProductId);
            return View(freightProductList);
        }

        // GET: FreightProductLists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freightProductList = await _context.FreightProductLists.FindAsync(id);
            if (freightProductList == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.FreightOutbounds, "OutboundOrderId", "OutboundOrderId", freightProductList.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Inventories, "ProductId", "ProductId", freightProductList.ProductId);
            return View(freightProductList);
        }

        // POST: FreightProductLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderId,ProductId,Quantity")] FreightProductList freightProductList)
        {
            if (id != freightProductList.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freightProductList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreightProductListExists(freightProductList.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.FreightOutbounds, "OutboundOrderId", "OutboundOrderId", freightProductList.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Inventories, "ProductId", "ProductId", freightProductList.ProductId);
            return View(freightProductList);
        }

        // GET: FreightProductLists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freightProductList = await _context.FreightProductLists
                .Include(f => f.FreightOutbound)
                .Include(f => f.Inventory)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (freightProductList == null)
            {
                return NotFound();
            }

            return View(freightProductList);
        }

        // POST: FreightProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var freightProductList = await _context.FreightProductLists.FindAsync(id);
            if (freightProductList != null)
            {
                _context.FreightProductLists.Remove(freightProductList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreightProductListExists(string id)
        {
            return _context.FreightProductLists.Any(e => e.OrderId == id);
        }
    }
}
