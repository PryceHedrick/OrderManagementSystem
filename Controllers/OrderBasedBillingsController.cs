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
    public class OrderBasedBillingsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderBasedBillingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderBasedBillings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderBasedBillings.Include(o => o.BillingAccount).Include(o => o.OrderBasedCharge);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderBasedBillings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedBilling = await _context.OrderBasedBillings
                .Include(o => o.BillingAccount)
                .Include(o => o.OrderBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (orderBasedBilling == null)
            {
                return NotFound();
            }

            return View(orderBasedBilling);
        }

        // GET: OrderBasedBillings/Create
        public IActionResult Create()
        {
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId");
            ViewData["OrderChargeId"] = new SelectList(_context.OrderBasedCharges, "OrderBasedChargeId", "OrderBasedChargeId");
            return View();
        }

        // POST: OrderBasedBillings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAccountId,OrderChargeId")] OrderBasedBilling orderBasedBilling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderBasedBilling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", orderBasedBilling.BillingAccountId);
            ViewData["OrderChargeId"] = new SelectList(_context.OrderBasedCharges, "OrderBasedChargeId", "OrderBasedChargeId", orderBasedBilling.OrderChargeId);
            return View(orderBasedBilling);
        }

        // GET: OrderBasedBillings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedBilling = await _context.OrderBasedBillings.FindAsync(id);
            if (orderBasedBilling == null)
            {
                return NotFound();
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", orderBasedBilling.BillingAccountId);
            ViewData["OrderChargeId"] = new SelectList(_context.OrderBasedCharges, "OrderBasedChargeId", "OrderBasedChargeId", orderBasedBilling.OrderChargeId);
            return View(orderBasedBilling);
        }

        // POST: OrderBasedBillings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BillingAccountId,OrderChargeId")] OrderBasedBilling orderBasedBilling)
        {
            if (id != orderBasedBilling.BillingAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderBasedBilling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderBasedBillingExists(orderBasedBilling.BillingAccountId))
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
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", orderBasedBilling.BillingAccountId);
            ViewData["OrderChargeId"] = new SelectList(_context.OrderBasedCharges, "OrderBasedChargeId", "OrderBasedChargeId", orderBasedBilling.OrderChargeId);
            return View(orderBasedBilling);
        }

        // GET: OrderBasedBillings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderBasedBilling = await _context.OrderBasedBillings
                .Include(o => o.BillingAccount)
                .Include(o => o.OrderBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (orderBasedBilling == null)
            {
                return NotFound();
            }

            return View(orderBasedBilling);
        }

        // POST: OrderBasedBillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderBasedBilling = await _context.OrderBasedBillings.FindAsync(id);
            if (orderBasedBilling != null)
            {
                _context.OrderBasedBillings.Remove(orderBasedBilling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderBasedBillingExists(string id)
        {
            return _context.OrderBasedBillings.Any(e => e.BillingAccountId == id);
        }
    }
}
