using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class BillingsController : Controller
    {
        private readonly AppDbContext _context;

        public BillingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Billings
        public async Task<IActionResult> Index(string searchString)
        {
            var billings = _context.Billings.Include(b => b.BillingAccount).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                billings = billings.Where(b => b.BillingId.Contains(searchString) ||
                                               b.BillingAccountId.Contains(searchString));
            }

            return View(await billings.ToListAsync());
        }

        // GET: Billings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.BillingAccount)
                .FirstOrDefaultAsync(m => m.BillingId == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // GET: Billings/Create
        public IActionResult Create()
        {
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId");
            return View();
        }

        // POST: Billings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingId,BillingAccountId,Amount,DateCreated")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", billing.BillingAccountId);
            return View(billing);
        }

        // GET: Billings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", billing.BillingAccountId);
            return View(billing);
        }

        // POST: Billings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BillingId,BillingAccountId,Amount,DateCreated")] Billing billing)
        {
            if (id != billing.BillingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingExists(billing.BillingId))
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
            ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId", billing.BillingAccountId);
            return View(billing);
        }

        // GET: Billings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.BillingAccount)
                .FirstOrDefaultAsync(m => m.BillingId == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // POST: Billings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var billing = await _context.Billings.FindAsync(id);
            if (billing != null)
            {
                _context.Billings.Remove(billing);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BillingExists(string id)
        {
            return _context.Billings.Any(e => e.BillingId == id);
        }
    }
}