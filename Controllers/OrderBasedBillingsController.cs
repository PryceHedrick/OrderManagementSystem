using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Threading.Tasks;

public class OrderBasedBillingController : Controller
{
    private readonly AppDbContext _context;

    public OrderBasedBillingController(AppDbContext context)
    {
        _context = context;
    }

    // GET: OrderBasedBilling
    public async Task<IActionResult> Index()
    {
        var orderBillings = await _context.OrderBasedBillings
            .Include(ob => ob.BillingAccount)
            .Include(ob => ob.OrderBasedCharge)
            .ToListAsync();
        return View(orderBillings);
    }

    // GET: OrderBasedBilling/Create
    public IActionResult Create()
    {
        ViewData["BillingAccountId"] = new SelectList(_context.BillingAccounts, "BillingAccountId", "BillingAccountId");
        ViewData["OrderChargeId"] = new SelectList(_context.OrderBasedCharges, "OrderChargeId", "OrderChargeId");
        return View();
    }

    // POST: OrderBasedBilling/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BillingAccountId,OrderChargeId")] OrderBasedBilling orderBilling)
    {
        if (ModelState.IsValid)
        {
            _context.Add(orderBilling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderBilling);
    }

    // GET: OrderBasedBilling/Edit/5
    public async Task<IActionResult> Edit(string billingAccountId, string orderChargeId)
    {
        if (billingAccountId == null || orderChargeId == null)
        {
            return NotFound();
        }

        var orderBilling = await _context.OrderBasedBillings
            .FirstOrDefaultAsync(m => m.BillingAccountId == billingAccountId && m.OrderChargeId == orderChargeId);
        if (orderBilling == null)
        {
            return NotFound();
        }
        return View(orderBilling);
    }

    // POST: OrderBasedBilling/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string billingAccountId, string orderChargeId, [Bind("BillingAccountId,OrderChargeId")] OrderBasedBilling orderBilling)
    {
        if (billingAccountId != orderBilling.BillingAccountId || orderChargeId != orderBilling.OrderChargeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderBilling);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderBasedBillingExists(orderBilling.BillingAccountId, orderBilling.OrderChargeId))
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
        return View(orderBilling);
    }

    // GET: OrderBasedBilling/Delete/5
    public async Task<IActionResult> Delete(string billingAccountId, string orderChargeId)
    {
        if (billingAccountId == null || orderChargeId == null)
        {
            return NotFound();
        }

        var orderBilling = await _context.OrderBasedBillings
            .FirstOrDefaultAsync(m => m.BillingAccountId == billingAccountId && m.OrderChargeId == orderChargeId);
        if (orderBilling == null)
        {
            return NotFound();
        }

        return View(orderBilling);
    }

    // POST: OrderBasedBilling/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string billingAccountId, string orderChargeId)
    {
        var orderBilling = await _context.OrderBasedBillings
            .FirstOrDefaultAsync(m => m.BillingAccountId == billingAccountId && m.OrderChargeId == orderChargeId);
        _context.OrderBasedBillings.Remove(orderBilling);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderBasedBillingExists(string billingAccountId, string orderChargeId)
    {
        return _context.OrderBasedBillings.Any(e => e.BillingAccountId == billingAccountId && e.OrderChargeId == orderChargeId);
    }
}