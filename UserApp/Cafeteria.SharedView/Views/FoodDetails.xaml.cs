using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetails
    {
        public FoodDetails()
        {
            InitializeComponent();
            SetupBehaviors();
        }

        private void SetupBehaviors()
        {
            AddReviewButton.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1,
                    Command = new Command(() => { AddReviewBlock.IsVisible = true; })
                });
            CancelReviewButton.Clicked += (sender, args) =>
            {
                AddReviewBlock.IsVisible = false;
            };
            SubmitReviewButton.Clicked += (sender, args) =>
            {
                AddReviewBlock.IsVisible = false;
                DisplayAlert("Success", "Review submitted", "Ok");
            };
        }
    }
}