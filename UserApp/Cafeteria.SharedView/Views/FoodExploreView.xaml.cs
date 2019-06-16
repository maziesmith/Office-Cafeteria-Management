using Cafeteria.SharedView.Services;
using Cafeteria.SharedView.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodExploreView
    {
        public FoodExploreView()
        {
            InitializeComponent();
        }

        private void OnFoodItemSelected(object sender, ItemTappedEventArgs e)
        {
            var foodItem = e.Item;
            var viewModel = (FoodExploreViewModel)BindingContext;
            viewModel.NavigateToDetails(new ConstructorParameter("foodItem", foodItem));
        }
    }
}