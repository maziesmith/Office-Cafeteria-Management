using Android.App;
using Android.Content;
using Cafeteria.SharedView.PlatformAbstractions;

namespace Cafeteria.Droid.Services
{
    internal class NotificationService : INotificationService
    {
        public void Notify(string title, string message)
        {
            // Instantiate the builder and set notification elements:
            var builder = new Notification.Builder(Application.Context)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.food_icon);

            // Build the notification:
            var notification = builder.Build();

            // Get the notification manager:
            var notificationManager =
               Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}