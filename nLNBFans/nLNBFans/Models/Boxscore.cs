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
    public class Boxscore
    {
        public int IDPlayer { get; set; }
        public string NombreCompleto { get; set; }
        public string Imagen { get; set; } = "Player.png";
        public string Condicion { get; set; }
        public int ID { get; set; }
        public int IDCalendario { get; set; }
        public int IDEquipo { get; set; }
        public decimal Minutos { get; set; }
        public decimal FP { get; set; }
        public decimal BR { get; set; }
        public decimal BP { get; set; }
        public decimal DO { get; set; }
        public decimal Asist { get; set; }
        public decimal ROF { get; set; }
        public decimal DEF { get; set; }
        public decimal REB { get; set; }
        public decimal TCI { get; set; }
        public decimal TCA { get; set; }
        public decimal PCTTC { get; set; }
        public decimal T3I { get; set; }
        public decimal T3A { get; set; }
        public decimal PCTT3 { get; set; }
        public decimal TLI { get; set; }
        public decimal TLA { get; set; }
        public decimal PCTTL { get; set; }
        public decimal Ptos { get; set; }
        public string Estatus { get; set; }

        [JsonIgnore]
        public string TC { get { return $"{string.Format("{0:0}", TCA)}/{string.Format("{0:0}", TCI)}"; } }
        [JsonIgnore]
        public string T3 { get { return $"{string.Format("{0:0}",T3A)}/{string.Format("{0:0}", T3I)}"; } }
        [JsonIgnore]
        public string TL { get { return $"{string.Format("{0:0}", TLA)}/{string.Format("{0:0}", TLI)}"; } }

        public ICommand SelectCommand
        {
            get
            {
                return new Command((e) => {
                    //var boxScore = (Boxscore)e;
                    
                    NavigationService navigationService = new NavigationService();
                    navigationService.Navegate("ProfilePlayerPage", null, e);
                });
            }
        }
    }
}
