using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cafeteria.SharedView.Abstractions;
using Cafeteria.SharedView.PlatformAbstractions;
using Xamarin.Forms;

namespace Cafeteria.SharedView.ViewModel
{
    public class OrderStatusViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;
        private readonly INotificationService _notificationService;
        private readonly IFoodRepository _foodRepository;
        private readonly INavigationService _navigationService;
        private string _status;
        private IDisposable _disposable;
        private bool _isFoodPrepared;

        public OrderStatusViewModel(Order order, IOrderService orderService,
            INotificationService notificationService, IFoodRepository foodRepository, INavigationService navigationService)
        {
            Order = order;
            _orderService = orderService;
            _notificationService = notificationService;
            _foodRepository = foodRepository;
            _navigationService = navigationService;
            FoodItem = foodRepository.GetFoodItems().FirstOrDefault(item => item.Id == order.FoodId);
            Task.Factory.StartNew(() =>
            {
                SendOrder().Wait();
                SubscribeStatus();
            });
        }

        private Task<bool> SendOrder()
        {
            return _orderService.SendOrder(Order);
        }

        private void SubscribeStatus()
        {
            _disposable = _orderService.GetOrderStatusObservable(Order.OrderId)
                .Subscribe(status =>
                {
                    Status = status.ToString();
                    CheckFoodPrepared(status);
                    if (status != OrderStatus.Prepared) return;
                    DisposeStream();
                    var foodItem = _foodRepository.GetFoodInfo(Order.FoodId);
                    _notificationService.Notify("Order Prepared", $"Your {foodItem.Details.Name} is ready to be collected at counter");
                });
        }

        private void CheckFoodPrepared(OrderStatus orderStatus)
        {
            IsFoodPrepared = orderStatus == OrderStatus.Prepared;
        }

        public bool IsFoodPrepared
        {
            get => _isFoodPrepared;
            set
            {
                _isFoodPrepared = value;
                OnPropertyChanged();
            }
        }

        private void DisposeStream()
        {
            _disposable?.Dispose();
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public Order Order { get; set; }

        public FoodItem FoodItem { get; set; }

        public ICommand HomeCommand
        {
            get
            {
                return new Command(o =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.ActionSelection);
                });
            }
        }
    }
}
