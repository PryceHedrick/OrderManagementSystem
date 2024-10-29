using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.BillingAccounts)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Email,DateCreated")] User user, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                user.Password = HashPassword(user.Password);
                user.DateCreated = DateTime.Now;

                // Assign roles
                if (selectedRoles != null)
                {
                    foreach (var roleId in selectedRoles)
                    {
                        user.UserRoles.Add(new UserRole { RoleId = roleId });
                    }
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "Name", selectedRoles);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "Name", user.UserRoles.Select(ur => ur.RoleId));
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Username,Password,Email,DateCreated")] User user, string[] selectedRoles)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                u => u.Username, u => u.Email))
            {
                try
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        // Hash the new password
                        userToUpdate.Password = HashPassword(user.Password);
                    }

                    // Update roles
                    UpdateUserRoles(selectedRoles, userToUpdate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "Name", selectedRoles);
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                // Remove associated UserRoles
                _context.UserRoles.RemoveRange(user.UserRoles);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private void UpdateUserRoles(string[] selectedRoles, User userToUpdate)
        {
            if (selectedRoles == null)
            {
                userToUpdate.UserRoles = new List<UserRole>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var userRoles = new HashSet<string>(userToUpdate.UserRoles.Select(r => r.RoleId));

            foreach (var role in _context.Roles)
            {
                if (selectedRolesHS.Contains(role.RoleId))
                {
                    if (!userRoles.Contains(role.RoleId))
                    {
                        userToUpdate.UserRoles.Add(new UserRole { UserId = userToUpdate.UserId, RoleId = role.RoleId });
                    }
                }
                else
                {
                    if (userRoles.Contains(role.RoleId))
                    {
                        var roleToRemove = userToUpdate.UserRoles.FirstOrDefault(r => r.RoleId == role.RoleId);
                        if (roleToRemove != null)
                        {
                            _context.UserRoles.Remove(roleToRemove);
                        }
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            // Simple SHA256 hashing (for demonstration purposes)
            // **Note:** Use a more secure hashing algorithm like BCrypt in production
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}