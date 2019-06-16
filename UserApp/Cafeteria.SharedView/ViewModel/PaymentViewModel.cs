using System.Windows.Input;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using Cafeteria.SharedView.Abstractions;
using Xamarin.Forms;
using System;
using Cafeteria.SharedView.Services;

namespace Cafeteria.SharedView.ViewModel
{
    public class PaymentViewModel
    {
        private readonly INavigationService _navigationService;

        public PaymentViewModel(FoodItem foodItem, INavigationService navigationService)
        {
            _navigationService = navigationService;
            SelectedFood = foodItem;
        }

        public ICommand ConfirmOrder
        {
            get
            {
                return new Command(() =>
                {
                    var order = new Order
                    {
                        FoodId = SelectedFood.Id,
                        Payment = new Payment()
                        {
                            Amount = SelectedFood.Details.Price,
                        }
                    };

                    _navigationService.NavigateTo(ViewModelLocator.OrderStatus,
                        new[] { new ConstructorParameter("order", order) });
                    
                });
            }
        }

        public FoodItem SelectedFood { get; set; }
    }
}
