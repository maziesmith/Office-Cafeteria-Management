using System;
using System.Collections.ObjectModel;
using System.Linq;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using Cafeteria.SharedView.Abstractions;
using Cafeteria.SharedView.Services;

namespace Cafeteria.SharedView.ViewModel
{
    public class FoodExploreViewModel
    {
        private readonly INavigationService _navigationService;

        public FoodExploreViewModel(IFoodRepository foodRepository, INavigationService navigationService)
        {
            _navigationService = navigationService;
            AllItems = new ObservableCollection<FoodItem>(foodRepository.GetFoodItems());
            AvailableItems = new ObservableCollection<FoodItem>(AllItems.Where(item => item.IsAvailableNow));
        }
        
        public void NavigateToDetails(ConstructorParameter parameter)
        {
            _navigationService.NavigateTo(ViewModelLocator.FoodDetails, new[] {parameter});
        }

        public ObservableCollection<FoodItem> AllItems { get; set; }

        public ObservableCollection<FoodItem> AvailableItems { get; set; }
    }
}
