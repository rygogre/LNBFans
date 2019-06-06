using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.ViewModels
{
    public class GameDayVM
    {
        public ObservableCollection<GameDay> GamesDay { get; set; }

        public GameDayVM()
        {
            //GamesDay = new ObservableCollection<GameDay>();
            //GetGameDay();
        }

        //public async Task<List<GameDay>> GetGameDay()
        //{
        //    GamesDay = new ObservableCollection<GameDay>();

        //    var gameDayService = new GameDayService();
        //    var games = await gameDayService.GetGameDay();

        //    foreach(var items in games)
        //    {
        //        GamesDay.Add(items);
        //    }

        //    return ;
        //}
    }
}
