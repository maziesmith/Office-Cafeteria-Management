using System.Collections.Generic;
using System.Threading.Tasks;
using Cafeteria.SharedView.Services;
using Cafeteria.SharedView.ViewModel;

namespace Cafeteria.SharedView.Abstractions
{
    public interface INavigationService
    {
        Task NavigateTo(ViewModelLocator viewModel, IEnumerable<ConstructorParameter> constructorParameters = null);
    }
}
