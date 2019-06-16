using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment
    {
        private Color _defaultBackground;

        public Payment()
        {
            InitializeComponent();
            SetupBehaviors();
        }

        private void SetupBehaviors()
        {
            _defaultBackground = CodButton.BackgroundColor;
            CodButton.Clicked += (sender, args) =>
            {
                ToggleVisibility(CodSelect);
                ToggleBackGround(CodButton);
            };
            WalletButton.Clicked += (sender, args) =>
            {
                ToggleVisibility(WalletSelect);
                ToggleBackGround(WalletButton);
            };
            VoucherButton.Clicked += (sender, args) =>
            {
                ToggleVisibility(VoucherSelect);
                ToggleBackGround(VoucherButton);
            };
        }

        private static void ToggleVisibility(VisualElement element)
        {
            element.IsVisible = !element.IsVisible;
        }

        private void ToggleBackGround(VisualElement element)
        {
            element.BackgroundColor = element.BackgroundColor == _defaultBackground ? Color.ForestGreen : _defaultBackground;
        }
    }
}