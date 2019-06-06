using nLNBFans.Pages;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.Models
{
    public class MenuItems
    {
        NavigationService navigationService;
        public string Icon { get; set; }
        public string Title { get; set; }
        public string TypeTarget { get; set; }

        public ICommand NavegateCommand
        {
            get { return new Command(() => navigationService.Navegate(TypeTarget)); 
            }
        }

        public MenuItems()
        {
            navigationService = new NavigationService();
        }

        //private void Navegate()
        //{
        //    App.Master.IsPresented = false;
        //    switch (TypeTarget)
        //    {                
        //        case "PlayerPage":
        //            App.Navigator.PushAsync(new PlayerPage());
        //            break;
        //        case "NewsPage":
        //            App.Navigator.PushAsync(new NewsPage());
        //            break;
        //        default:
        //            break;

        //    }
        //}
    }
}
