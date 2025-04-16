using CaloryMonitor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    //Добавяме админ акаунта в базата данни
    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string adminEmail = "admin@calorymonitor.bg";
        string adminPassword = "admin123";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

        if (existingAdmin == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Грешка при създаване на админ: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
    
    //Добавяме храни в базата данни
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Foods.Any())
            {
                return; // Вече има храни, няма нужда от повторно добавяне
            }

            var foods = new List<Food>
{
    new Food { Name = "Пилешко филе", CaloriesPer100g = 165, ProteinsPer100g = 31, CarbsPer100g = 0, FatsPer100g = 3.6, Status = FoodStatus.NonVegan },
    new Food { Name = "Ориз", CaloriesPer100g = 130, ProteinsPer100g = 2.7, CarbsPer100g = 28, FatsPer100g = 0.3, Status = FoodStatus.Vegan },
    new Food { Name = "Яйца", CaloriesPer100g = 155, ProteinsPer100g = 13, CarbsPer100g = 1.1, FatsPer100g = 11, Status = FoodStatus.NonVegan },
    new Food { Name = "Сирене", CaloriesPer100g = 402, ProteinsPer100g = 25, CarbsPer100g = 1.3, FatsPer100g = 33, Status = FoodStatus.NonVegan },
    new Food { Name = "Мляко", CaloriesPer100g = 42, ProteinsPer100g = 3.4, CarbsPer100g = 5, FatsPer100g = 1, Status = FoodStatus.NonVegan },
    new Food { Name = "Хляб бял", CaloriesPer100g = 265, ProteinsPer100g = 9, CarbsPer100g = 49, FatsPer100g = 3.2, Status = FoodStatus.Vegan },
    new Food { Name = "Хляб пълнозърнест", CaloriesPer100g = 247, ProteinsPer100g = 13, CarbsPer100g = 41, FatsPer100g = 4.2, Status = FoodStatus.Vegan },
    new Food { Name = "Банани", CaloriesPer100g = 89, ProteinsPer100g = 1.1, CarbsPer100g = 23, FatsPer100g = 0.3, Status = FoodStatus.Vegan },
    new Food { Name = "Ябълки", CaloriesPer100g = 52, ProteinsPer100g = 0.3, CarbsPer100g = 14, FatsPer100g = 0.2, Status = FoodStatus.Vegan },
    new Food { Name = "Грозде", CaloriesPer100g = 69, ProteinsPer100g = 0.7, CarbsPer100g = 18, FatsPer100g = 0.2, Status = FoodStatus.Vegan },
    new Food { Name = "Авокадо", CaloriesPer100g = 160, ProteinsPer100g = 2, CarbsPer100g = 9, FatsPer100g = 15, Status = FoodStatus.Vegan },
    new Food { Name = "Шоколад", CaloriesPer100g = 546, ProteinsPer100g = 4.9, CarbsPer100g = 61, FatsPer100g = 31, Status = FoodStatus.Synthetic },
    new Food { Name = "Кисело мляко", CaloriesPer100g = 59, ProteinsPer100g = 3.5, CarbsPer100g = 4.7, FatsPer100g = 3.3, Status = FoodStatus.NonVegan },
    new Food { Name = "Картофи", CaloriesPer100g = 77, ProteinsPer100g = 2, CarbsPer100g = 17, FatsPer100g = 0.1, Status = FoodStatus.Vegan },
    new Food { Name = "Червени чушки", CaloriesPer100g = 31, ProteinsPer100g = 1, CarbsPer100g = 6, FatsPer100g = 0.3, Status = FoodStatus.Vegan },
    new Food { Name = "Домати", CaloriesPer100g = 18, ProteinsPer100g = 0.9, CarbsPer100g = 3.9, FatsPer100g = 0.2, Status = FoodStatus.Vegan },
    new Food { Name = "Краставици", CaloriesPer100g = 16, ProteinsPer100g = 0.7, CarbsPer100g = 3.6, FatsPer100g = 0.1, Status = FoodStatus.Vegan },
    new Food { Name = "Моркови", CaloriesPer100g = 41, ProteinsPer100g = 0.9, CarbsPer100g = 10, FatsPer100g = 0.2, Status = FoodStatus.Vegan },
    new Food { Name = "Броколи", CaloriesPer100g = 34, ProteinsPer100g = 2.8, CarbsPer100g = 6.6, FatsPer100g = 0.4, Status = FoodStatus.Vegan },
    new Food { Name = "Спанак", CaloriesPer100g = 23, ProteinsPer100g = 2.9, CarbsPer100g = 3.6, FatsPer100g = 0.4, Status = FoodStatus.Vegan },
    new Food { Name = "Царевица", CaloriesPer100g = 96, ProteinsPer100g = 3.4, CarbsPer100g = 21, FatsPer100g = 1.5, Status = FoodStatus.Vegan },
    new Food { Name = "Леща", CaloriesPer100g = 116, ProteinsPer100g = 9, CarbsPer100g = 20, FatsPer100g = 0.4, Status = FoodStatus.Vegan },
    new Food { Name = "Фасул", CaloriesPer100g = 127, ProteinsPer100g = 8.7, CarbsPer100g = 22, FatsPer100g = 0.5, Status = FoodStatus.Vegan },
    new Food { Name = "Черен шоколад", CaloriesPer100g = 600, ProteinsPer100g = 7.8, CarbsPer100g = 46, FatsPer100g = 42, Status = FoodStatus.Synthetic },
    new Food { Name = "Колбас", CaloriesPer100g = 300, ProteinsPer100g = 12, CarbsPer100g = 4, FatsPer100g = 26, Status = FoodStatus.NonVegan },
    new Food { Name = "Салам", CaloriesPer100g = 350, ProteinsPer100g = 15, CarbsPer100g = 2, FatsPer100g = 30, Status = FoodStatus.NonVegan },
    new Food { Name = "Свинско месо", CaloriesPer100g = 242, ProteinsPer100g = 27, CarbsPer100g = 0, FatsPer100g = 14, Status = FoodStatus.NonVegan },
    new Food { Name = "Говеждо месо", CaloriesPer100g = 250, ProteinsPer100g = 26, CarbsPer100g = 0, FatsPer100g = 15, Status = FoodStatus.NonVegan },
    new Food { Name = "Сьомга", CaloriesPer100g = 208, ProteinsPer100g = 20, CarbsPer100g = 0, FatsPer100g = 13, Status = FoodStatus.NonVegan },
    new Food { Name = "Риба тон", CaloriesPer100g = 132, ProteinsPer100g = 28, CarbsPer100g = 0, FatsPer100g = 1.3, Status = FoodStatus.NonVegan },
    new Food { Name = "Маслини", CaloriesPer100g = 115, ProteinsPer100g = 0.8, CarbsPer100g = 6, FatsPer100g = 11, Status = FoodStatus.Vegan },
    new Food { Name = "Слънчогледови семки", CaloriesPer100g = 584, ProteinsPer100g = 21, CarbsPer100g = 20, FatsPer100g = 51, Status = FoodStatus.Vegan },
    new Food { Name = "Ядки", CaloriesPer100g = 607, ProteinsPer100g = 20, CarbsPer100g = 21, FatsPer100g = 54, Status = FoodStatus.Vegan },
    new Food { Name = "Бадеми", CaloriesPer100g = 579, ProteinsPer100g = 21, CarbsPer100g = 22, FatsPer100g = 50, Status = FoodStatus.Vegan },
    new Food { Name = "Фъстъци", CaloriesPer100g = 567, ProteinsPer100g = 25, CarbsPer100g = 16, FatsPer100g = 49, Status = FoodStatus.Vegan },
    new Food { Name = "Кашу", CaloriesPer100g = 553, ProteinsPer100g = 18, CarbsPer100g = 30, FatsPer100g = 44, Status = FoodStatus.Vegan },
    new Food { Name = "Кока-Кола", CaloriesPer100g = 42, ProteinsPer100g = 0, CarbsPer100g = 11, FatsPer100g = 0, Status = FoodStatus.Synthetic },
    new Food { Name = "Енергийна напитка", CaloriesPer100g = 45, ProteinsPer100g = 0, CarbsPer100g = 11, FatsPer100g = 0, Status = FoodStatus.Synthetic },
    new Food { Name = "Чай", CaloriesPer100g = 1, ProteinsPer100g = 0, CarbsPer100g = 0, FatsPer100g = 0, Status = FoodStatus.Vegan },
    new Food { Name = "Кафе", CaloriesPer100g = 1, ProteinsPer100g = 0.1, CarbsPer100g = 0, FatsPer100g = 0, Status = FoodStatus.Vegan },
    new Food { Name = "Бира", CaloriesPer100g = 43, ProteinsPer100g = 0.5, CarbsPer100g = 3.6, FatsPer100g = 0, Status = FoodStatus.Synthetic },
    new Food { Name = "Вино", CaloriesPer100g = 85, ProteinsPer100g = 0.1, CarbsPer100g = 2.6, FatsPer100g = 0, Status = FoodStatus.Synthetic },
    new Food { Name = "Пица", CaloriesPer100g = 266, ProteinsPer100g = 11, CarbsPer100g = 33, FatsPer100g = 10, Status = FoodStatus.Synthetic },
    new Food { Name = "Хотдог", CaloriesPer100g = 290, ProteinsPer100g = 10, CarbsPer100g = 24, FatsPer100g = 20, Status = FoodStatus.Synthetic },
    new Food { Name = "Бургер", CaloriesPer100g = 295, ProteinsPer100g = 13, CarbsPer100g = 30, FatsPer100g = 14, Status = FoodStatus.Synthetic },
    new Food { Name = "Пържени картофи", CaloriesPer100g = 312, ProteinsPer100g = 3.4, CarbsPer100g = 41, FatsPer100g = 15, Status = FoodStatus.Synthetic },
    new Food { Name = "Кроасан", CaloriesPer100g = 406, ProteinsPer100g = 8, CarbsPer100g = 45, FatsPer100g = 21, Status = FoodStatus.Synthetic },
    new Food { Name = "Палачинки", CaloriesPer100g = 227, ProteinsPer100g = 6, CarbsPer100g = 28, FatsPer100g = 10, Status = FoodStatus.Synthetic }
};


            context.Foods.AddRange(foods);
            context.SaveChanges();
        }
    }
}
