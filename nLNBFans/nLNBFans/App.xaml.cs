using nLNBFans.Interfaces;
using nLNBFans.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace nLNBFans
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            Helpers.SettingHelper.TemporadaActual();
            Helpers.SettingHelper.CreateTags();

            var token = Preferences.Get("appToken", string.Empty);
            if (token.ToString().Equals(string.Empty))
                RegisterDevice();

            
            var detail = new NavigationPage(new ConnectivityPage() { Title = "Conectividad" });

            if (Helpers.SettingHelper.ConnectionDevice()) //Valido la conectividad...
                detail = new NavigationPage(new MainPage() { Title = "LNB Fans" });
            
            
            MainPage = new MasterDetailPage()
            {
                Master = new MasterPage() { Title = "LNB Fans" },
                Detail = detail
            };

        }

        public void RegisterDevice()
        {
            //Para version ANDROID enviamos registrar TOKEN and Tags.
            if (Device.RuntimePlatform == Device.Android)
            {
                var register = DependencyService.Get<IRegisterDevice>();
                register.RegisterDevice();
            }
        }

        protected override void OnStart ()
		{
           
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
