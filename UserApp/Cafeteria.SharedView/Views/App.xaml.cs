using System;
using Autofac;
using Autofac.Features.ResolveAnything;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.Services;
using Cafeteria.SharedView.Abstractions;
using Cafeteria.SharedView.Services;
using Cafeteria.SharedView.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cafeteria.SharedView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App
    {
        public static IContainer Container { get; private set; }
        private ContainerBuilder _builder;

        public event EventHandler<ContainerBuilder> BuildingContainer;

        public App()
        {
            InitializeComponent();
        }

        private void ShowMainPage()
        {
            MainPage = new NavigationPage { BarBackgroundColor = Color.LightGray };
            var navigation = Container.Resolve<INavigationService>();
            navigation.NavigateTo(ViewModelLocator.Login);
        }

        private void InitializeIoc()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterType<NavigationService>().As<INavigationService>();
            _builder.RegisterType<FoodRepository>().As<IFoodRepository>().InstancePerLifetimeScope();
            _builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            _builder.RegisterType<EventService>().As<IEventService>().InstancePerLifetimeScope();
            _builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            BuildingContainer?.Invoke(this, _builder);
            Container = _builder.Build();
        }
        

        protected override void OnStart()
        {
            InitializeIoc();
            ShowMainPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static int AnimationSpeed = 250;
    }
}