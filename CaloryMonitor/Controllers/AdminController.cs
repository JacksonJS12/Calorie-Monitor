using CaloryMonitor.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloryMonitor.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string AdminEmail = "admin@calorymonitor.bg";

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> IsAdmin()
        {
            var user = await _userManager.GetUserAsync(User);
            return user != null && user.Email == AdminEmail;
        }

        public async Task<IActionResult> Foods()
        {
            if (!await IsAdmin()) return Forbid();

            var foods = await _context.Foods.OrderBy(f => f.Name).ToListAsync();
            return View(foods);
        }

        //Този метод изкарва View за създаване на нова храна в базата данни
        public async Task<IActionResult> Create()
        {
            if (!await IsAdmin()) return Forbid();

            return View(new Food());
        }

        //Този метод създава храната
        [HttpPost]
        public async Task<IActionResult> Create(Food food)
        {
            if (!await IsAdmin()) return Forbid();

            if (!ModelState.IsValid)
                return View(food);

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Успешно добавена храна!";
            return RedirectToAction("Foods");
        }

        //Този метод изкарва View за редактиране на дадена храна
        public async Task<IActionResult> Edit(int id)
        {
            if (!await IsAdmin()) return Forbid();

            var food = await _context.Foods.FindAsync(id);
            if (food == null) return NotFound();

            return View(food);
        }

        //Този метод редактира храната
        [HttpPost]
        public async Task<IActionResult> Edit(Food food)
        {
            if (!await IsAdmin()) return Forbid();

            if (!ModelState.IsValid)
                return View(food);

            _context.Foods.Update(food);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Успешно редактирана храна!";
            return RedirectToAction("Foods");
        }

        //Този метод изтрива дадена храна
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await IsAdmin()) return Forbid();

            var food = await _context.Foods.FindAsync(id);
            if (food == null) return NotFound();

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Храната беше изтрита.";
            return RedirectToAction("Foods");
        }
    }


}
