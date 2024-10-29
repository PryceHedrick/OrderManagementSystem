using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class BillingAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public BillingAccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BillingAccounts
        public async Task<IActionResult> Index(string searchString)
        {
            var billingAccounts = _context.BillingAccounts.Include(ba => ba.User).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                billingAccounts = billingAccounts.Where(ba => ba.BillingAccountId.Contains(searchString) ||
                                                             ba.User.Username.Contains(searchString));
            }

            return View(await billingAccounts.ToListAsync());
        }

        // GET: BillingAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAccount = await _context.BillingAccounts
                .Include(ba => ba.User)
                .Include(ba => ba.Billings)
                .Include(ba => ba.CostBasedBillings)
                .Include(ba => ba.OrderBasedBillings)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (billingAccount == null)
            {
                return NotFound();
            }

            return View(billingAccount);
        }

        // GET: BillingAccounts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: BillingAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAccountId,UserId,AccountBalance")] BillingAccount billingAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billingAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", billingAccount.UserId);
            return View(billingAccount);
        }

        // GET: BillingAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAccount = await _context.BillingAccounts.FindAsync(id);
            if (billingAccount == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", billingAccount.UserId);
            return View(billingAccount);
        }

        // POST: BillingAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BillingAccountId,UserId,AccountBalance")] BillingAccount billingAccount)
        {
            if (id != billingAccount.BillingAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingAccountExists(billingAccount.BillingAccountId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", billingAccount.UserId);
            return View(billingAccount);
        }

        // GET: BillingAccounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAccount = await _context.BillingAccounts
                .Include(ba => ba.User)
                .FirstOrDefaultAsync(m => m.BillingAccountId == id);
            if (billingAccount == null)
            {
                return NotFound();
            }

            return View(billingAccount);
        }

        // POST: BillingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var billingAccount = await _context.BillingAccounts.FindAsync(id);
            if (billingAccount != null)
            {
                _context.BillingAccounts.Remove(billingAccount);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BillingAccountExists(string id)
        {
            return _context.BillingAccounts.Any(e => e.BillingAccountId == id);
        }
    }
}