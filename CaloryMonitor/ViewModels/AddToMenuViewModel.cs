using System.ComponentModel.DataAnnotations;

namespace CaloryMonitor.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddToMenuViewModel
    {
        public int SelectedFoodId { get; set; }

        [Range(1, 10000, ErrorMessage = "Моля, въведете валиден грамаж.")]
        public double QuantityGrams { get; set; }

        public List<Food> AvailableFoods { get; set; }

        [Required(ErrorMessage = "Моля, изберете секция на деня.")]
        public string TimeOfTheDay { get; set; }

        public List<string> TimeOptions { get; } = new List<string> { "Сутрин", "Обед", "Вечер" };
    }

}

