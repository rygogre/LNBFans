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
    public class LeaderShort
    {
        public int IDPlayer { get; set; }
        public string NombrePlayer { get; set; }
        public string Imagen { get; set; }
        public string NombreEquipo { get; set; }
        public decimal JJ { get; set; }
        public string Area { get; set; }
        public string AreaValor { get; set; }
        public string AreaValorPromedio { get; set; }

        public ICommand SelectCommand
        {
            get
            {
                return new Command((e) => {                    
                    NavigationService navigationService = new NavigationService();
                    navigationService.Navegate("ProfilePlayerPage", null, e);
                });
            }
        }
    }
}
