namespace CaloryMonitor.ViewModels
{
    public class MenuWithItemsViewModel
    {
        public DateTime Date { get; set; }
        public List<MenuItemViewModel> Items { get; set; } = new();
    }

    public class MenuItemViewModel
    {
        public string FoodName { get; set; }
        public double QuantityGrams { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public string TimeOfTheDay { get; set; }
    }

}
