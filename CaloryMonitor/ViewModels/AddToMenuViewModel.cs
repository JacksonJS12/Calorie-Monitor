using System.ComponentModel.DataAnnotations;

namespace CaloryMonitor.ViewModels
{
    public class AddToMenuViewModel
    {
        public int SelectedFoodId { get; set; }

        [Range(1, 10000)]
        public double QuantityGrams { get; set; }

        public List<Food>? AvailableFoods { get; set; }
    }
}
