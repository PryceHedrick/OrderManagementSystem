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
    public class CostBasedBillingController : Controller
    {
        private readonly AppDbContext _context;

        public CostBasedBillingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CostBasedBilling
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CostBasedBillings.Include(c => c.BillingAccount).Include(c => c.CostBasedCharge);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CostBasedBilling/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings
                .Include(c => c.BillingAccount)
                .Include(c => c.CostBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (costBasedBilling == null)
            {
                return NotFound();
            }

            return View(costBasedBilling);
        }

        // GET: CostBasedBilling/Create
        public IActionResult Create()
        {
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId");
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "CostChargeId");
            return View();
        }

        // POST: CostBasedBilling/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAccountId,CostChargeId")] CostBasedBilling costBasedBilling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costBasedBilling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", costBasedBilling.BillingAccountId);
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "CostChargeId", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // GET: CostBasedBilling/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings.FindAsync(id);
            if (costBasedBilling == null)
            {
                return NotFound();
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", costBasedBilling.BillingAccountId);
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "CostChargeId", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // POST: CostBasedBilling/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BillingAccountId,CostChargeId")] CostBasedBilling costBasedBilling)
        {
            if (id != costBasedBilling.BillingAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costBasedBilling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostBasedBillingExists(costBasedBilling.BillingAccountId))
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
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", costBasedBilling.BillingAccountId);
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "CostChargeId", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // GET: CostBasedBilling/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings
                .Include(c => c.BillingAccount)
                .Include(c => c.CostBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (costBasedBilling == null)
            {
                return NotFound();
            }

            return View(costBasedBilling);
        }

        // POST: CostBasedBilling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var costBasedBilling = await _context.CostBasedBillings.FindAsync(id);
            if (costBasedBilling != null)
            {
                _context.CostBasedBillings.Remove(costBasedBilling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostBasedBillingExists(string id)
        {
            return _context.CostBasedBillings.Any(e => e.BillingAccountId == id);
        }
    }
}
