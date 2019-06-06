using nLNBFans.Models;
using nLNBFans.ViewModels;
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
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();

            MainVM mainVM = new MainVM(null);
            BindingContext = mainVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var param = Preferences.Get("pushParam", string.Empty);
            //DisplayAlert("yo", "SOY PARAM" + param, "OK");
            if (!string.IsNullOrEmpty(param))
            {
                NavegationNotification(param);
            }
        }

        private async void NavegationNotification(string param)
        {
            string[] paramPush = param.Split(';');
            Preferences.Set("pushParam", string.Empty);

            try
            {
                if (paramPush.Length > 2)
                {
                    string valorNav = paramPush.GetValue(1).ToString();
                    object val = paramPush.GetValue(2).ToString();
                    Services.NavigationService nav = new Services.NavigationService();

                    switch (valorNav)
                    {
                        case "B":
                            var gamesDay = new List<GameDay>();
                            var gameDay = new GameDay();
                            Services.GameDayService gameDayService = new Services.GameDayService();
                            gamesDay = await gameDayService.GetGameDay(null);

                            foreach (var game in gamesDay)
                            {
                                if (int.Parse(val.ToString()) == game.IDCalendario)
                                    gameDay = game as Models.GameDay;
                            }

                            if (gameDay.IDCalendario > 0)
                                nav.Navegate("GameDetailsPage", gameDay);

                            break;

                        case "N":
                            nav.Navegate("NewsPage");
                            break;

                        case "E":
                            nav.Navegate("LeadersPage");
                            break;

                        case "C":
                            nav.Navegate("SchedulePage");
                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {

            }
        }
    }
}