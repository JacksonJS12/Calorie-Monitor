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

        //Този метод показва View за добавяне на рподукт към менюто
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
        //Този метод добавя продукта към даденото меню
        [HttpPost]
        public async Task<IActionResult> AddToMenu(AddToMenuViewModel model)
        {
            // Дефинираме речник за преобразуване
            var timeMapping = new Dictionary<string, string>
            {
                { "Сутрин", "Morning" },
                { "Обед", "Afternoon" },
                { "Вечер", "Evening" }
            };

            // Преобразуваме стойността от dropdown (на български) към английски
            string englishTime;
            if (!timeMapping.TryGetValue(model.TimeOfTheDay, out englishTime))
            {
                ModelState.AddModelError("TimeOfTheDay", "Невалидна секция");
                model.AvailableFoods = await _context.Foods.ToListAsync();
                return View(model);
            }

            // Намираме потребителя и/или менюто за днешната дата
            var userId = GetUserId(); 
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

            // Създаваме MenuItem, като използваме английската стойност
            var menuItem = new MenuItem
            {
                MenuId = menu.Id,
                FoodId = model.SelectedFoodId,
                QuantityGrams = model.QuantityGrams,
                TimeOfTheDay = englishTime
            };

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Храната е добавена към менюто ти за днес!";
            return RedirectToAction(nameof(MyMenus));
        }

        //Този метод показва View със всички менюта
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
                    Calories = i.Food.CaloriesPer100g * i.QuantityGrams / 100,
                    Proteins = i.Food.ProteinsPer100g * i.QuantityGrams / 100,
                    Carbs = i.Food.CarbsPer100g * i.QuantityGrams / 100,
                    Fats = i.Food.FatsPer100g * i.QuantityGrams / 100,
                    TimeOfTheDay = i.TimeOfTheDay
                }).ToList()
            }).ToList();


            return View(viewModel);
        }
    }
}
