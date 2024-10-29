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
    public class OrderBasedChargesController : Controller
    {
        private readonly AppDbContext _context;

        public OrderBasedChargesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderBasedCharges
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderBasedCharges.ToListAsync());
        }

        // GET: OrderBasedCharges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedCharge = await _context.OrderBasedCharges
                .FirstOrDefaultAsync(m => m.OrderChargeId == id);
            if (orderBasedCharge == null)
            {
                return NotFound();
            }

            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderBasedCharges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderChargeId,Amount,Description")] OrderBasedCharge orderBasedCharge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderBasedCharge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedCharge = await _context.OrderBasedCharges.FindAsync(id);
            if (orderBasedCharge == null)
            {
                return NotFound();
            }
            return View(orderBasedCharge);
        }

        // POST: OrderBasedCharges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderChargeId,Amount,Description")] OrderBasedCharge orderBasedCharge)
        {
            if (id != orderBasedCharge.OrderChargeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderBasedCharge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderBasedChargeExists(orderBasedCharge.OrderChargeId))
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
            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedCharge = await _context.OrderBasedCharges
                .FirstOrDefaultAsync(m => m.OrderChargeId == id);
            if (orderBasedCharge == null)
            {
                return NotFound();
            }

            return View(orderBasedCharge);
        }

        // POST: OrderBasedCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderBasedCharge = await _context.OrderBasedCharges.FindAsync(id);
            if (orderBasedCharge != null)
            {
                _context.OrderBasedCharges.Remove(orderBasedCharge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderBasedChargeExists(string id)
        {
            return _context.OrderBasedCharges.Any(e => e.OrderChargeId == id);
        }
    }
}
