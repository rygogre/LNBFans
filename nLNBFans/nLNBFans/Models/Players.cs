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
    public class Players
    {
        NavigationService navigationService;
        public int IDPlayer { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Posicion { get; set; }
        public double Pesos { get; set; }
        public string Estatura { get; set; }
        public string Imagen { get; set; }
        public string Estatus { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string Condicion { get; set; }
        public string IDEquipo { get; set; }
        public string ImagenEquipo { get; set; }
        public string AnoNacimiento { get; set; }        
        [JsonIgnore]
        public string NombreCompleto { get { return PrimerNombre + " " + PrimerApellido; } }

        public ICommand SelectCommand
        {
            get
            {
                return new Command((e) => {
                    var player = (Players)e;

                    navigationService = new NavigationService();
                    navigationService.Navegate("ProfilePlayerPage", null, player.IDPlayer);
                });
            }
        }
    }
}
