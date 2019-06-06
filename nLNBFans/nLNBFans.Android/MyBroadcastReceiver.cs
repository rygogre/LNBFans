using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using WindowsAzure.Messaging;
using Xamarin.Essentials;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace nLNBFans.Droid.Implementations
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]

    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { Constants.SenderID };

        public const string TAG = "MyBroadcastReceiver-GCM";
    }
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        #region Attributes

        private NotificationHub Hub { get; set; }
        public static string RegistrationID { get; private set; }
        #endregion

        //#region Methods
        public PushHandlerService() : base(Constants.SenderID)
        {
            Log.Info(MyBroadcastReceiver.TAG, "PushHandlerService() constructor");

        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(MyBroadcastReceiver.TAG, "GCM Message Received!");

            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            var message = intent.Extras.GetString("Message");
            var title = intent.Extras.GetString("title");
            var param = intent.Extras.GetString("param")?.ToString();
            var secuencia = intent.Extras.GetString("secuencia")?.ToString();

            if (!string.IsNullOrEmpty(message))
            {
                createNotification(title, string.Format("{0}", message), param, secuencia);
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(MyBroadcastReceiver.TAG, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(MyBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            Preferences.Set("appToken", registrationId);
            RegistrationID = registrationId;

            //Services.NotificacionService notificationService = new Services.NotificacionService();
            //notificationService.OnRegistered(registrationId);
            
            //Enviar Token Azure...
            Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString, context);

            try
            {

                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadcastReceiver.TAG, ex.Message);
            }
            
            string[] tags = new [] { Preferences.Get("appTags", string.Empty) };
            if (tags.Length == 0)
                tags = new string[] { "0,1,2,3,4,5" };

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags);
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);
        }

        void createNotification(string title, string desc, string param, string secuencia)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show UI
            var uiIntent = new Intent(this, typeof(MainActivity));

            //agregando el parametro extra para obetener valores de la notifiacation.

            uiIntent.PutExtra("param", param);
            uiIntent.PutExtra("secuencia", secuencia);

            //PendingIntent.GetActivity()
            string[] paramsPush = param.Split(';');
            int sec = 0;
            if (int.TryParse(paramsPush.GetValue(0).ToString(), out sec))
                sec = int.Parse(paramsPush.GetValue(0).ToString());
            //Create the notification
            //var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);
            var builder = new NotificationCompat.Builder(this);
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, sec, uiIntent, 0))
                     .SetSmallIcon(Resource.Drawable.icon)
                     .SetTicker(title)
                     .SetContentTitle(title)
                     .SetContentText(desc)

                     //Set the notification sound
                     .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                     //Auto cancel will remove the notification once the user touches it
                     .SetAutoCancel(true).Build();

            //Show the notification  
            notificationManager.Notify(sec, notification);
            dialogNotify(title, desc);
        }

        protected void dialogNotify(String title, String message)
        {
            var mainActivity = MainActivity.GetInstance();
            mainActivity.RunOnUiThread(() =>
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(mainActivity);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(title);
                alert.SetButton("Aceptar", delegate
                {
                    alert.Dismiss();
                });
                alert.SetMessage(message);
                alert.Show();
            });
        }
    }
}