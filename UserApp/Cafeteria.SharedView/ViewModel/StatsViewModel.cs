using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.SharedView.ViewModel
{
    public class StatsViewModel
    {
        private readonly List<FoodItem> _orderedFood;
        public StatsViewModel(IOrderService orderService, IFoodRepository foodRepository)
        {
            var orders = orderService.GetPastOrders().Result;
            _orderedFood = orders.Select(order => foodRepository.GetFoodInfo(order.FoodId)).ToList();
            CalculateSpendStats();
            CalculateHealthStats();
        }

        public ObservableCollection<ChartData> MoneyDistribution { get; set; }

        public ObservableCollection<ChartData> CountDistribution { get; set; }

        public double TotalMoneySpent { get; set; }

        public double PerMonthSpent { get; set; }

        public string MostBought { get; set; }

        public string MostSpentOn { get; set; }

        private void CalculateSpendStats()
        {
            TotalMoneySpent = _orderedFood.Sum(item => item.Details.Price);
            PerMonthSpent = TotalMoneySpent / 2;

            var groupedFood = _orderedFood.GroupBy(item => item.Id);

            var moneyDistribution = groupedFood.Select(group =>
            {
                var food = group.First();
                return new ChartData
                {
                    Name = food.Details.Name,
                    Value = food.Details.Price * group.Count() * 100 / TotalMoneySpent
                };
            });
            var enumerable = moneyDistribution as IList<ChartData> ?? moneyDistribution.ToList();
            MostSpentOn =
                enumerable.Aggregate((spend1, spend2) => spend1.Value > spend2.Value ? spend1 : spend2).Name;
            MoneyDistribution = new ObservableCollection<ChartData>(enumerable);

            var totalTimesOrdered = _orderedFood.Count;
            var countDistribution = groupedFood.Select(group =>
            {
                var food = group.First();
                return new ChartData
                {
                    Name = food.Details.Name,
                    Value = (double)group.Count() * 100 / totalTimesOrdered
                };
            });

            var distribution = countDistribution as IList<ChartData> ?? countDistribution.ToList();
            MostBought =
                distribution.Aggregate((spend1, spend2) => spend1.Value > spend2.Value ? spend1 : spend2).Name;
            CountDistribution = new ObservableCollection<ChartData>(distribution);
        }

        public int CaloriesPerDay { get; set; }

        public int CaloriesPerMeal { get; set; }

        public ObservableCollection<ChartData> CaloriesDistribution { get; set; }

        public ObservableCollection<ChartData> CaloriesNumbers { get; set; }

        public string HighestCaloriesFood { get; set; }

        private void CalculateHealthStats()
        {
            var totalCalories = _orderedFood.Sum(item => item.Details.Calories);
            CaloriesPerMeal = totalCalories / _orderedFood.Count;
            CaloriesPerDay = CaloriesPerMeal * 2;

            var groupedFood = _orderedFood.GroupBy(item => item.Id);

            var enumerable = groupedFood as IList<IGrouping<int, FoodItem>> ?? groupedFood.ToList();
            var caloriesDistribution = enumerable.Select(group =>
            {
                var food = group.First();
                return new ChartData
                {
                    Name = food.Details.Name,
                    Value = (double)food.Details.Calories * group.Count() * 100 / totalCalories
                };
            });
            CaloriesDistribution = new ObservableCollection<ChartData>(caloriesDistribution);

            var caloriesNumber = enumerable.Select(group =>
            {
                var food = group.First();
                return new ChartData
                {
                    Name = food.Details.Name,
                    Value = food.Details.Calories
                };
            });
            HighestCaloriesFood = caloriesNumber.Aggregate((data1, data2) => data1.Value > data2.Value ? data1 : data2)
                .Name;
            CaloriesNumbers = new ObservableCollection<ChartData>(caloriesNumber);
        }
    }
}
