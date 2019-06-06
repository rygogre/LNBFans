using nLNBFans.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.Models
{
    public class GameDay
    {
        public int IDCalendario { get; set; }
        public DateTime Fecha { get; set; }
        public string LogoEquipo { get; set; }
        public string LogoVisita { get; set; }
        public string Visita { get; set; }
        public string Casa { get; set; }
        public int ScoreVisita { get; set; }
        public int ScoreCasa { get; set; }
        public string NombreInstalacion { get; set; }
        public string Ciudad { get; set; }
        public string FechaNueva { get; set; }
        public int IDEquipoVisita { get; set; }
        public int IDEquipoCasa { get; set; }
        public string ColorEquipoVisita { get; set; }
        public string ColorEquipoCasa { get; set; }
        public string Hora { get; set; }        
        public string RecordVisita { get; set; } = "0-0";
        public string RecordCasa { get; set; } = "0-0";
        [JsonIgnore]
        public string ScoreFinal {
            get
            {
                if (Visita != "0")
                    return $"{ScoreVisita} - {ScoreCasa}";
                else
                    return Hora;
            }
        }

        public ICommand SelectCommand
        {
            get
            {
                return new Command((e) =>
                {
                    NavigationService navigationService = new NavigationService();
                    navigationService.Navegate("GameDetailsPage", e);
                });
            }
        }
    }
}
