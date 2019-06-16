using System.Windows.Input;
using Cafeteria.SharedView.Abstractions;
using Xamarin.Forms;

namespace Cafeteria.SharedView.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SignInCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.ActionSelection);
                });
            }
        }
    }
}
