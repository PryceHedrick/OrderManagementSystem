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
    public class CostBasedChargeController : Controller
    {
        private readonly AppDbContext _context;

        public CostBasedChargeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CostBasedCharge
        public async Task<IActionResult> Index()
        {
            return View(await _context.CostBasedCharges.ToListAsync());
        }

        // GET: CostBasedCharge/Details/5
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

        // GET: CostBasedCharge/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostBasedCharge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostChargeId,Amount,Description")] CostBasedCharge costBasedCharge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costBasedCharge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costBasedCharge);
        }

        // GET: CostBasedCharge/Edit/5
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

        // POST: CostBasedCharge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CostChargeId,Amount,Description")] CostBasedCharge costBasedCharge)
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

        // GET: CostBasedCharge/Delete/5
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

        // POST: CostBasedCharge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var costBasedCharge = await _context.CostBasedCharges.FindAsync(id);
            if (costBasedCharge != null)
            {
                _context.CostBasedCharges.Remove(costBasedCharge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostBasedChargeExists(string id)
        {
            return _context.CostBasedCharges.Any(e => e.CostChargeId == id);
        }
    }
}
