using System.Windows.Input;
using Cafeteria.SharedView.Abstractions;
using Xamarin.Forms;

namespace Cafeteria.SharedView.ViewModel
{
    class ActionSelectionViewModel
    {
        private readonly INavigationService _navigationService;

        public ActionSelectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand NavigateCommand
        {
            get
            {
                return new Command((commandParam) =>
                {
                    _navigationService.NavigateTo((ViewModelLocator) commandParam);
                });
            }
        }
    }
}
