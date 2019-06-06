using nLNBFans.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.Models
{
    public class Schedule : GameDay
    {
        [JsonIgnore]
        public string GameResult
        {
            get
            {
                return this.ScoreVisita + " - " + this.ScoreCasa;
            }
        }

        public Command SelectCommandDetail
        {
            get
            {
                return new Command((e) =>
                {
                    var game = (GameDay)e;
                    NavigationService navigationService = new NavigationService();
                    navigationService.Navegate("GameDetailsPage", e);
                });
            }
        }
    }
}
