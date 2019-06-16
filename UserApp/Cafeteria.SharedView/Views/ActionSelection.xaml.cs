using Cafeteria.SharedView.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionSelection : ContentPage
    {
        public ActionSelection()
        {
            InitializeComponent();
            SetBehaviors();
        }

        private void SetBehaviors()
        {
            FoodOrderButton.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(() =>
                    {
                        ((ActionSelectionViewModel) BindingContext).NavigateCommand.Execute(ViewModelLocator.FoodExplore);
                    })
                });
            Wallet.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(() =>
                    {
                        ((ActionSelectionViewModel)BindingContext).NavigateCommand.Execute(ViewModelLocator.Wallet);
                    })
                });

            CafeteriaEventsButton.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(() =>
                    {
                        ((ActionSelectionViewModel)BindingContext).NavigateCommand.Execute(ViewModelLocator.Events);
                    })
                });

            StatsButton.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(() =>
                    {
                        ((ActionSelectionViewModel)BindingContext).NavigateCommand.Execute(ViewModelLocator.Stats);
                    })
                });
        }
    }
}