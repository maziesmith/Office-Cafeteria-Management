using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Cafeteria.Droid.Services;
using Cafeteria.SharedView.PlatformAbstractions;
using Cafeteria.SharedView.Views;

namespace Cafeteria.Droid
{
    [Activity(Label = "MS Cafeteria", MainLauncher = true, Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            var app = new App();
            app.BuildingContainer += (sender, builder) => RegisterServices(builder);
            LoadApplication(app);
        }

        private static void RegisterServices(ContainerBuilder appBuilder)
        {
            appBuilder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
        }
    }
}

