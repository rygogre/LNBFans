using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WindowsAzure.Messaging;
using Xamarin.Essentials;

namespace nLNBFans.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private SBNotificationHub Hub { get; set; }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //PushNotification
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                       UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                       new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
            //endPUSH



            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(125, 125, 125);
            UITabBar.Appearance.TintColor = UIColor.White;

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Hub = new SBNotificationHub(Constants.ListenConnectionString, Constants.NotificationHubName);

            Hub.UnregisterAllAsync(deviceToken, (error) => {
                if (error != null)
                {
                    Console.WriteLine("Error calling Unregister: {0}", error.ToString());
                    return;
                }

                Preferences.Set("appToken", deviceToken.ToString());

                string[] myTags = new[] { Preferences.Get("appTags", string.Empty) };
                if (myTags.Length == 0)
                    myTags = new string[] { "0,1,2,3,4,5" };


                //NSSet tags = null; // create tags if you want
                //NSSet tags = new NSSet(new string[] { "0,", "1,", "2,", "3,", "4,", "5" });
                NSSet tags = new NSSet(myTags);


                Hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) => {
                    if (errorCallback != null)
                    {
                        //LNBFans.Helpers.SettingHelper.CreateTagFile();
                        Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                    }
                });
            });
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotification(userInfo, false);
        }


        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;
                string title = string.Empty;
                string param = "Inicial";

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("title")))
                    title = (aps[new NSString("title")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("param")))
                {
                    Preferences.Set("pushParam", (aps[new NSString("param")] as NSString).ToString());
                    //param = (aps[new NSString("param")] as NSString).ToString();
                }

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        UIAlertView avAlert = new UIAlertView(title, alert, null, "OK", null);
                        avAlert.Show();
                    }
                }
            }
        }
    }
}
