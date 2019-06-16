using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeed);

            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await SignInButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await Register.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
        }
    }
}