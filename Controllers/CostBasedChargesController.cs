using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class CostBasedChargesController : Controller
    {
        private readonly AppDbContext _context;

        public CostBasedChargesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CostBasedCharges
        public async Task<IActionResult> Index()
        {
            return View(await _context.CostBasedCharges.ToListAsync());
        }

        // GET: CostBasedCharges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedCharge = await _context.CostBasedCharges
                .FirstOrDefaultAsync(m => m.CostChargeId == id);
            if (costBasedCharge == null)
            {
                return NotFound();
            }

            return View(costBasedCharge);
        }

        // GET: CostBasedCharges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostBasedCharges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostChargeId,Description,Amount")] CostBasedCharge costBasedCharge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costBasedCharge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costBasedCharge);
        }

        // GET: CostBasedCharges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedCharge = await _context.CostBasedCharges.FindAsync(id);
            if (costBasedCharge == null)
            {
                return NotFound();
            }
            return View(costBasedCharge);
        }

        // POST: CostBasedCharges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CostChargeId,Description,Amount")] CostBasedCharge costBasedCharge)
        {
            if (id != costBasedCharge.CostChargeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costBasedCharge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostBasedChargeExists(costBasedCharge.CostChargeId))
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
            return View(costBasedCharge);
        }

        // GET: CostBasedCharges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costBasedCharge = await _context.CostBasedCharges
                .FirstOrDefaultAsync(m => m.CostChargeId == id);
            if (costBasedCharge == null)
            {
                return NotFound();
            }

            return View(costBasedCharge);
        }

        // POST: CostBasedCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var costBasedCharge = await _context.CostBasedCharges.FindAsync(id);
            if (costBasedCharge != null)
            {
                _context.CostBasedCharges.Remove(costBasedCharge);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CostBasedChargeExists(string id)
        {
            return _context.CostBasedCharges.Any(e => e.CostChargeId == id);
        }
    }
}