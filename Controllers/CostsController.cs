// Controllers/CostController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class CostController : Controller
    {
        private readonly AppDbContext _context;

        public CostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cost
        public async Task<IActionResult> Index(string searchString)
        {
            var costs = from c in _context.Costs
                        select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                costs = costs.Where(c => c.CostId.Contains(searchString) || c.Description.Contains(searchString));
            }

            return View(await costs.ToListAsync());
        }

        // GET: Cost/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.CostId == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Cost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostId,Description,Amount")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cost);
        }

        // GET: Cost/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.FindAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            return View(cost);
        }

        // POST: Cost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CostId,Description,Amount")] Cost cost)
        {
            if (id != cost.CostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.CostId))
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
            return View(cost);
        }

        // GET: Cost/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.CostId == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Cost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cost = await _context.Costs.FindAsync(id);
            if (cost != null)
            {
                _context.Costs.Remove(cost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CostExists(string id)
        {
            return _context.Costs.Any(e => e.CostId == id);
        }
    }
}