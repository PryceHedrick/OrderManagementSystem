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
    public class OrderBasedChargeController : Controller
    {
        private readonly AppDbContext _context;

        public OrderBasedChargeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderBasedCharge
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderBasedCharges.Include(o => o.Charge).Include(o => o.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderBasedCharge/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedCharge = await _context.OrderBasedCharges
                .Include(o => o.Charge)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderBasedChargeId == id);
            if (orderBasedCharge == null)
            {
                return NotFound();
            }

            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharge/Create
        public IActionResult Create()
        {
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: OrderBasedCharge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderBasedChargeId,OrderId,ChargeId,Amount,DateCharged")] OrderBasedCharge orderBasedCharge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderBasedCharge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", orderBasedCharge.ChargeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderBasedCharge.OrderId);
            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharge/Edit/5
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
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", orderBasedCharge.ChargeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderBasedCharge.OrderId);
            return View(orderBasedCharge);
        }

        // POST: OrderBasedCharge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderBasedChargeId,OrderId,ChargeId,Amount,DateCharged")] OrderBasedCharge orderBasedCharge)
        {
            if (id != orderBasedCharge.OrderBasedChargeId)
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
                    if (!OrderBasedChargeExists(orderBasedCharge.OrderBasedChargeId))
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
            ViewData["ChargeId"] = new SelectList(_context.Charges, "ChargeId", "ChargeId", orderBasedCharge.ChargeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderBasedCharge.OrderId);
            return View(orderBasedCharge);
        }

        // GET: OrderBasedCharge/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedCharge = await _context.OrderBasedCharges
                .Include(o => o.Charge)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderBasedChargeId == id);
            if (orderBasedCharge == null)
            {
                return NotFound();
            }

            return View(orderBasedCharge);
        }

        // POST: OrderBasedCharge/Delete/5
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
            return _context.OrderBasedCharges.Any(e => e.OrderBasedChargeId == id);
        }
    }
}
