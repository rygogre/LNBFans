using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using nLNBFans.Interfaces;
using Xamarin.Forms;
using Gcm.Client;

[assembly: Dependency(typeof(nLNBFans.Droid.RegistrationDevice))]
namespace nLNBFans.Droid
{
    public class RegistrationDevice : IRegisterDevice
    {
        public void RegisterDevice()
        {
            var mainActivity = MainActivity.GetInstance();
            GcmClient.CheckDevice(mainActivity);
            GcmClient.CheckManifest(mainActivity);

            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(mainActivity, Constants.SenderID);
        }
    }
}