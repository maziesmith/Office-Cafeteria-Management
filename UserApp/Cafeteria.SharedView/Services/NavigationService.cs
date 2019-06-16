using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Cafeteria.SharedView.Abstractions;
using Cafeteria.SharedView.ViewModel;
using Cafeteria.SharedView.Views;
using Xamarin.Forms;

namespace Cafeteria.SharedView.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IContainer _container = App.Container;
        public async Task NavigateTo(ViewModelLocator viewModelName, IEnumerable<ConstructorParameter> constructorParameters = null)
        {
            object viewModel;
            Page view;
            switch (viewModelName)
            {
                case ViewModelLocator.Login:
                    {
                        viewModel = _container.Resolve<LoginViewModel>();
                        view = new Login();
                        break;
                    }
                case ViewModelLocator.ActionSelection:
                    {
                        viewModel = _container.Resolve<ActionSelectionViewModel>();
                        view = new ActionSelection();
                        break;
                    }

                case ViewModelLocator.FoodExplore:
                    {
                        viewModel = _container.Resolve<FoodExploreViewModel>();
                        view = new FoodExploreView();
                        break;
                    }

                case ViewModelLocator.FoodDetails:
                    {
                        viewModel = _container.Resolve<FoodDetailsViewModel>(GetParameters(constructorParameters));
                        view = new FoodDetails();
                        break;
                    }
                case ViewModelLocator.Payment:
                    {
                        viewModel = _container.Resolve<PaymentViewModel>(GetParameters(constructorParameters));
                        view = new Payment();
                        break;
                    }
                case ViewModelLocator.OrderStatus:
                    {
                        viewModel = _container.Resolve<OrderStatusViewModel>(GetParameters(constructorParameters));
                        view = new OrderStatus();
                        break;
                    }

                case ViewModelLocator.Wallet:
                    {
                        viewModel = _container.Resolve<WalletViewModel>();
                        view = new Wallet();
                        break;
                    }

                case ViewModelLocator.Events:
                    {
                        viewModel = _container.Resolve<CafeteriaEventsViewModel>();
                        view = new CafeteriaEvents();
                        break;
                    }

                case ViewModelLocator.Stats:
                    {
                        viewModel = _container.Resolve<StatsViewModel>();
                        view = new Stats();
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
            view.BindingContext = viewModel;
            await Application.Current.MainPage.Navigation.PushAsync(view);

        }

        private static IEnumerable<NamedParameter> GetParameters(IEnumerable<ConstructorParameter> constructorParameters)
        {
            return constructorParameters?.Select(parameter => new NamedParameter(parameter.Name, parameter.Value));
        }
    }
}