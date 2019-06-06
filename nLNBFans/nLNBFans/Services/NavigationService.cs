using nLNBFans.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nLNBFans.Services
{
    public class NavigationService
    {
        public NavigationService()
        {
            //constructor....
        }

        public async void Navegate(string typeTarget, object parameter = null, object parameter1 = null)
        {
            var page = Application.Current.MainPage as MasterDetailPage;
            page.IsPresented = false;

            if (Helpers.SettingHelper.ConnectionDevice())
            {                
                switch (typeTarget)
                {
                    case "MainPage":
                        page.Detail =  new NavigationPage(new MainPage());
                        //await App.Navigator.PopToRootAsync();
                        break;
                    case "PlayerPage":
                        page.Detail = new NavigationPage(new PlayerPage());
                        //await App.Navigator.PushAsync(new PlayersPage());
                        break;
                    case "NewsPage":
                        page.Detail = new NavigationPage(new NewsPage());
                        //await App.Navigator.PushAsync(new NewsPage());
                        break;
                    case "NewsDetailPage":
                        await page.Detail.Navigation.PushAsync(new NewsDetailPage(parameter));
                        //await App.Navigator.PushAsync(new NewsDetailPage(parameter));
                        break;
                    case "SchedulePage":
                        page.Detail = new NavigationPage(new SchedulePage());
                        //await App.Navigator.PushAsync(new SchedulePage());
                        break;
                    case "TeamPage":
                        page.Detail = new NavigationPage(new TeamPage());
                        //await App.Navigator.PushAsync(new TeamPage());
                        break;
                    case "TeamDetailPage":
                        await page.Detail.Navigation.PushAsync(new TeamDetailPage((int)parameter, parameter1.ToString()));
                        //await App.Navigator.PushAsync(new TeamDetailPage((int)parameter, parameter1.ToString()));
                        break;
                    case "GameDetailsPage":
                        await page.Detail.Navigation.PushAsync(new GameDetailsPage(parameter));
                        //await App.Navigator.PushAsync(new GameDetailsPage(parameter));
                        break;
                    case "ProfilePlayerPage":
                        await page.Detail.Navigation.PushAsync(new ProfilePlayerPage(parameter, (int)parameter1));
                        //await App.Navigator.PushAsync(new ProfilePlayerPage(parameter, (int)parameter1));
                        break;
                    case "LeadersPage":
                        page.Detail = new NavigationPage(new LeadersPage());
                        //await App.Navigator.PushAsync(new LeadersPage());
                        break;
                    case "PreferenciasPage":
                        page.Detail = new NavigationPage(new PreferenciasPage());
                        //await App.Navigator.PushAsync(new PreferenciasPage());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await Navigate(new ConnectivityPage());
            }
        }

        private static async Task Navigate<T>(T page) where T : Page
        {
            NavigationPage.SetHasBackButton(page, false);
            NavigationPage.SetBackButtonTitle(page, "Back");

            //NavigationPage nav = new NavigationPage(new Pa());
            //await App.Navigator.PushAsync(page);
        }
    }
}
