using nLNBFans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nLNBFans.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferenciasPage : ContentPage
	{
		public PreferenciasPage ()
		{
			InitializeComponent ();            
            Preferencias();
            noticiasSwitch.Toggled += NoticiasSwitch_Toggled;
            inicioPartidosSwitch.Toggled += NoticiasSwitch_Toggled;
            finalPartidosSwitch.Toggled += NoticiasSwitch_Toggled;
            contratacionesSwitch.Toggled += NoticiasSwitch_Toggled;
            lesionesSwitch.Toggled += NoticiasSwitch_Toggled;
            //testSwitch.Toggled += NoticiasSwitch_Toggled;
        }

        private void NoticiasSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            SetPreferencias();
        }

        void Preferencias()
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;

            var tags = Preferences.Get("appTags", string.Empty);
            string rTags = "0,1,2,3,4,5";

            if (tags.ToString().Equals(string.Empty))
                Preferences.Set("appTags", "0,1,2,4,5");

            if (string.IsNullOrEmpty(tags))
            {
                Preferences.Set("appTags", rTags);
                tags = rTags;
            }
            
            string[] myTags = rTags.Split(',');
            if (myTags.Contains("1"))
                noticiasSwitch.IsToggled = true;
            if (myTags.Contains("2"))
                inicioPartidosSwitch.IsToggled = true;
            if (myTags.Contains("3"))
                finalPartidosSwitch.IsToggled = true;
            if (myTags.Contains("4"))
                contratacionesSwitch.IsToggled = true;
            if (myTags.Contains("5"))
                lesionesSwitch.IsToggled = true;
            //if (myTags.Contains("100"))
            //    testSwitch.IsToggled = true;

            if (Device.RuntimePlatform == Device.Android)
            {
                var register = DependencyService.Get<IRegisterDevice>();
                register.RegisterDevice();
            }

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
        }

        void SetPreferencias()
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;

            StringBuilder tags = new StringBuilder();

            tags.Append("0");

            if (noticiasSwitch.IsToggled)
                tags.Append(",1");
            if (inicioPartidosSwitch.IsToggled)
                tags.Append(",2");
            if (finalPartidosSwitch.IsToggled)
                tags.Append(",3");
            if (contratacionesSwitch.IsToggled)
                tags.Append(",4");
            if (lesionesSwitch.IsToggled)
                tags.Append(",5");
            //if (testSwitch.IsToggled)
            //    tags.Append(",100");

            try
            {
                Preferences.Set("appTags", tags.ToString());
            }
            catch { }

            if (Device.RuntimePlatform == Device.Android)
            {
                var register = DependencyService.Get<IRegisterDevice>();
                register.RegisterDevice();
            }

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
        }
    }
}