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
    public class InboundOrderController : Controller
    {
        private readonly AppDbContext _context;

        public InboundOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InboundOrder
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InboundOrders.Include(i => i.Creator).Include(i => i.Warehouse);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InboundOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inboundOrder = await _context.InboundOrders
                .Include(i => i.Creator)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.InboundOrderId == id);
            if (inboundOrder == null)
            {
                return NotFound();
            }

            return View(inboundOrder);
        }

        // GET: InboundOrder/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "WarehouseId");
            return View();
        }

        // POST: InboundOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InboundOrderId,OrderStatus,CreatorId,WarehouseId,EstimatedArrival,ProductQuantity,CreationDate,Cost,Currency,Boxes,InboundType,TrackingNumber,ReferenceOrderNumber,ArrivalMethod")] InboundOrder inboundOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inboundOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "UserId", "UserId", inboundOrder.CreatorId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "WarehouseId", inboundOrder.WarehouseId);
            return View(inboundOrder);
        }

        // GET: InboundOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inboundOrder = await _context.InboundOrders.FindAsync(id);
            if (inboundOrder == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "UserId", "UserId", inboundOrder.CreatorId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "WarehouseId", inboundOrder.WarehouseId);
            return View(inboundOrder);
        }

        // POST: InboundOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("InboundOrderId,OrderStatus,CreatorId,WarehouseId,EstimatedArrival,ProductQuantity,CreationDate,Cost,Currency,Boxes,InboundType,TrackingNumber,ReferenceOrderNumber,ArrivalMethod")] InboundOrder inboundOrder)
        {
            if (id != inboundOrder.InboundOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inboundOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InboundOrderExists(inboundOrder.InboundOrderId))
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
            ViewData["CreatorId"] = new SelectList(_context.Users, "UserId", "UserId", inboundOrder.CreatorId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "WarehouseId", inboundOrder.WarehouseId);
            return View(inboundOrder);
        }

        // GET: InboundOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inboundOrder = await _context.InboundOrders
                .Include(i => i.Creator)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.InboundOrderId == id);
            if (inboundOrder == null)
            {
                return NotFound();
            }

            return View(inboundOrder);
        }

        // POST: InboundOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inboundOrder = await _context.InboundOrders.FindAsync(id);
            if (inboundOrder != null)
            {
                _context.InboundOrders.Remove(inboundOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InboundOrderExists(string id)
        {
            return _context.InboundOrders.Any(e => e.InboundOrderId == id);
        }
    }
}
