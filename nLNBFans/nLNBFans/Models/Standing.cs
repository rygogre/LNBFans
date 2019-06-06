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
    public class Standing
    {
        public int IDEquipo { get; set; }
        public string NombreEquipo { get; set; }
        public string Logo { get; set; }
        public int JJ { get; set; }
        public int Ganados { get; set; }
        public int Perdidos { get; set; }
        public double Porcentaje { get; set; }
        public double Diferencia { get; set; }
        public string Casa { get; set; }
        public string Ruta { get; set; }        
        public string PathImage => string.Format("{0} {1}", "http://deportes56.com/Imagenes/Equipos/LNB/", Logo);

        public ICommand SelectCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigationService navigationService = new NavigationService();
                    navigationService.Navegate("TeamDetailPage", IDEquipo, NombreEquipo);
                });
            }
        }

    }
}
