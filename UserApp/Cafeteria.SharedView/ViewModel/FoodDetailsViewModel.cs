using System.Windows.Input;
using Cafeteria.CoreLibs.DomainModel;
using Cafeteria.SharedView.Abstractions;
using Cafeteria.SharedView.Services;
using Xamarin.Forms;

namespace Cafeteria.SharedView.ViewModel
{
    public class FoodDetailsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public FoodDetailsViewModel(FoodItem foodItem, INavigationService navigationService)
        {
            _navigationService = navigationService;
            FoodItem = foodItem;
        }

        public FoodItem FoodItem { get; set; }

        public ICommand OrderCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.Payment, new[] { new ConstructorParameter("foodItem", FoodItem) });
                });
            }
        }
    }
}
