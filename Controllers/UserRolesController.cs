using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index()
        {
            var userRoles = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role);
            return View(await userRoles.ToListAsync());
        }

        // GET: UserRoles/Details/UserId/RoleId
        public async Task<IActionResult> Details(string userId, string roleId)
        {
            if (userId == null || roleId == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: UserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", userRole.RoleId);
            return View(userRole);
        }

        // GET: UserRoles/Edit/UserId/RoleId
        public async Task<IActionResult> Edit(string userId, string roleId)
        {
            if (userId == null || roleId == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles.FindAsync(userId, roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", userRole.RoleId);
            return View(userRole);
        }

        // POST: UserRoles/Edit/UserId/RoleId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, string roleId, [Bind("UserId,RoleId")] UserRole userRole)
        {
            if (userId != userRole.UserId || roleId != userRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.UserId, userRole.RoleId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", userRole.RoleId);
            return View(userRole);
        }

        // GET: UserRoles/Delete/UserId/RoleId
        public async Task<IActionResult> Delete(string userId, string roleId)
        {
            if (userId == null || roleId == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // POST: UserRoles/Delete/UserId/RoleId
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId, string roleId)
        {
            var userRole = await _context.UserRoles.FindAsync(userId, roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleExists(string userId, string roleId)
        {
            return _context.UserRoles.Any(e => e.UserId == userId && e.RoleId == roleId);
        }
    }
}