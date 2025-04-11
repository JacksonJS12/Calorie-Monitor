using CaloryMonitor.Data;
using CaloryMonitor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloryMonitor.Controllers
{
    public class MenuController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
            : base(userManager)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> AddToMenu()
        {
            var foods = await _context.Foods.ToListAsync();

            var viewModel = new AddToMenuViewModel
            {
                AvailableFoods = foods
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMenu(AddToMenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableFoods = await _context.Foods.ToListAsync();
                return View(model);
            }

            var userId = GetUserId();

            // Намираме или създаваме меню за днешната дата
            var today = DateTime.Today;
            var menu = await _context.Menus
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.Date == today);

            if (menu == null)
            {
                menu = new Menu { UserId = userId, Date = today };
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
            }

            // Добавяме избраната храна
            var menuItem = new MenuItem
            {
                MenuId = menu.Id,
                FoodId = model.SelectedFoodId,
                QuantityGrams = model.QuantityGrams
            };

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Храната е добавена към менюто ти за днес!";
            return RedirectToAction("MyMenus");
        }
        public async Task<IActionResult> MyMenus()
        {
            var userId = GetUserId();

            var menus = await _context.Menus
                .Where(m => m.UserId == userId)
                .Include(m => m.Items)
                    .ThenInclude(i => i.Food)
                .OrderByDescending(m => m.Date)
                .ToListAsync();

            var viewModel = menus.Select(menu => new MenuWithItemsViewModel
            {
                Date = menu.Date,
                Items = menu.Items.Select(i => new MenuItemViewModel
                {
                    FoodName = i.Food.Name,
                    QuantityGrams = i.QuantityGrams,
                    Calories = (i.Food.CaloriesPer100g * i.QuantityGrams) / 100
                }).ToList()
            }).ToList();

            return View(viewModel);
        }
    }
}
