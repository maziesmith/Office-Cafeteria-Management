using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cafeteria.SharedView.Custom
{
    public class CustomButton : ContentView
    {

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomButton));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly BindableProperty CommandParameterProperty = 
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomButton));
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ButtonPressedEffect();
                    Command?.Execute(CommandParameter);
                });
            }
        }

        private async Task ButtonPressedEffect()
        {
            Opacity = 0.8;
            await Content.ScaleTo(0.95, 50, Easing.BounceIn);
            await Task.Delay(20);
            await Content.ScaleTo(1, 50, Easing.BounceOut);
            Opacity = 1;
        }

        public CustomButton()
        {
            Initialize();
        }


        public void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }

    }
}
