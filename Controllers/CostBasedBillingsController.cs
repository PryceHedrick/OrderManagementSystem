using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class CostBasedBillingsController : Controller
    {
        private readonly AppDbContext _context;

        public CostBasedBillingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CostBasedBillings
        public async Task<IActionResult> Index()
        {
            var costBasedBillings = _context.CostBasedBillings
                .Include(cbb => cbb.BillingAccount)
                .Include(cbb => cbb.CostBasedCharge);
            return View(await costBasedBillings.ToListAsync());
        }

        // GET: CostBasedBillings/Details/BillingAccountId/CostChargeId
        public async Task<IActionResult> Details(string billingAccountId, string costChargeId)
        {
            if (billingAccountId == null || costChargeId == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings
                .Include(cbb => cbb.BillingAccount)
                .Include(cbb => cbb.CostBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == billingAccountId && m.CostChargeId == costChargeId);
            if (costBasedBilling == null)
            {
                return NotFound();
            }

            return View(costBasedBilling);
        }

        // GET: CostBasedBillings/Create
        public IActionResult Create()
        {
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId");
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "Description");
            return View();
        }

        // POST: CostBasedBillings/Create
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
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "Description", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // GET: CostBasedBillings/Edit/BillingAccountId/CostChargeId
        public async Task<IActionResult> Edit(string billingAccountId, string costChargeId)
        {
            if (billingAccountId == null || costChargeId == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings.FindAsync(billingAccountId, costChargeId);
            if (costBasedBilling == null)
            {
                return NotFound();
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", costBasedBilling.BillingAccountId);
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "Description", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // POST: CostBasedBillings/Edit/BillingAccountId/CostChargeId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string billingAccountId, string costChargeId, [Bind("BillingAccountId,CostChargeId")] CostBasedBilling costBasedBilling)
        {
            if (billingAccountId != costBasedBilling.BillingAccountId || costChargeId != costBasedBilling.CostChargeId)
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
                    if (!CostBasedBillingExists(costBasedBilling.BillingAccountId, costBasedBilling.CostChargeId))
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
            ViewData["CostChargeId"] = new SelectList(_context.CostBasedCharges, "CostChargeId", "Description", costBasedBilling.CostChargeId);
            return View(costBasedBilling);
        }

        // GET: CostBasedBillings/Delete/BillingAccountId/CostChargeId
        public async Task<IActionResult> Delete(string billingAccountId, string costChargeId)
        {
            if (billingAccountId == null || costChargeId == null)
            {
                return NotFound();
            }

            var costBasedBilling = await _context.CostBasedBillings
                .Include(cbb => cbb.BillingAccount)
                .Include(cbb => cbb.CostBasedCharge)
                .FirstOrDefaultAsync(m => m.BillingAccountId == billingAccountId && m.CostChargeId == costChargeId);
            if (costBasedBilling == null)
            {
                return NotFound();
            }

            return View(costBasedBilling);
        }

        // POST: CostBasedBillings/Delete/BillingAccountId/CostChargeId
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string billingAccountId, string costChargeId)
        {
            var costBasedBilling = await _context.CostBasedBillings.FindAsync(billingAccountId, costChargeId);
            if (costBasedBilling != null)
            {
                _context.CostBasedBillings.Remove(costBasedBilling);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CostBasedBillingExists(string billingAccountId, string costChargeId)
        {
            return _context.CostBasedBillings.Any(e => e.BillingAccountId == billingAccountId && e.CostChargeId == costChargeId);
        }
    }
}