using CaloryMonitor.Data;
using CaloryMonitor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloryMonitor.Controllers
{
    public class BmiController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public BmiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
           : base(userManager)
        {
            _context = context;
        }

        //Този метод показва View със всички записи на BMI на потребителя
        public async Task<IActionResult> All()
        {
            var userId = GetUserId();

            var records = await _context.BMIRecords
                .Where(r => r.UserId == userId)
                .OrderBy(r => r.Date)
                .ToListAsync();

            ViewBag.BmiLabels = records.Select(r => r.Date.ToString("yyyy-MM-dd")).ToArray();
            ViewBag.BmiValues = records.Select(r => r.Bmi).ToArray();

            return View(records);
        }

        //Този метод показва View за добавяне на нов BMI запис
        public IActionResult Add()
        {
            return View(new BmiRecord { Date = DateTime.Today });
        }

        //Този метод добавя BMI запис
        [HttpPost]
        public async Task<IActionResult> Add(BmiRecord record)
        {
            record.UserId = GetUserId();
            _context.BMIRecords.Add(record);
            await _context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        //Този метод показва View за редактиране на BMI запис
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _context.BMIRecords.FindAsync(id);
            if (record == null || record.UserId != GetUserId())
                return NotFound();

            return View(record);
        }

        //Този метод редактира BMI запис
        [HttpPost]
        public async Task<IActionResult> Edit(BmiRecord record)
        {
            var existing = await _context.BMIRecords.FindAsync(record.Id);
            if (existing == null || existing.UserId != GetUserId())
                return NotFound();

            existing.Date = record.Date;
            existing.WeightKg = record.WeightKg;
            existing.HeightMeters = record.HeightMeters;

            await _context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        //Този метод изтрива BMI запис
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.BMIRecords.FindAsync(id);
            if (record == null || record.UserId != GetUserId())
                return NotFound();

            _context.BMIRecords.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction("All");
        }
    }

}
