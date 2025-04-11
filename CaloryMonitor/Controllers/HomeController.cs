using System.Diagnostics;
using CaloryMonitor.Data;
using CaloryMonitor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloryMonitor.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
            :base(userManager)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Ако потребителят не е логнат, просто зареждаме View
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            // Извличаме потребителското ID
            var userId = GetUserId();
            var today = DateTime.Today;

            // Намираме менюто на потребителя за днес (ако съществува)
            var menu = await _context.Menus
                .Include(m => m.Items)
                .ThenInclude(i => i.Food)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.Date == today);

            // Пресмятаме общия прием на калории
            double totalCalories = 0;
            if (menu != null)
            {
                totalCalories = menu.Items.Sum(i => i.Food.CaloriesPer100g * i.QuantityGrams / 100);
            }

            // Предаваме калориите във ViewBag.
            ViewBag.TotalCaloriesToday = totalCalories;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
