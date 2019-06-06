using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class LeadersVM : INotifyPropertyChanged
    {
        public List<string> years;
        private List<LeaderShort> leaderShort;
        private bool isBusy;
        private string publicidadHTML;

        public List<string> Years
        {
            get { return years; }
            set { years = value; OnPropertyChanged(); }
        }

        public List<LeaderShort> LeaderShort
        {
            get { return leaderShort; }
            set { leaderShort = value; OnPropertyChanged(); }
        }
        
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public string PublicidadHTML
        {
            get { return publicidadHTML; }
            set { publicidadHTML = value; OnPropertyChanged(); }
        }

        public Command AreaSelectCommand
        {
            get
            {
                return new Command((e) => {
                    string nada = e.ToString();
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LeadersVM(string equipo, string area, string temporada, string etapa)
        {
            //Years = Helpers.SettingHelper.Temporadas();
            GetLeaders(equipo, area, SetAreaService(area), temporada, SetEtapaService(etapa));
            GetPublicidad();

        }

        //public LeadersVM(string area, string etapa)
        //{
        //    //Years = Helpers.SettingHelper.Temporadas();
        //    GetLeaders(area, SetAreaService(area), Years.ElementAt(1).ToString(), SetEtapaService(etapa));
        //}

        public async void GetLeaders(string equipo, string area, string areaService, string temporada, string etapa)
        {
            IsBusy = true;
            
            LeaderService leaderService = new LeaderService();
            List<Leader> leaders = new List<Leader>();
            

            leaders = await leaderService.GetLeaders(equipo, areaService, equipo, temporada, etapa);

            if (leaders.Count > 0)
            {
                LeaderShort = new List<LeaderShort>();
                string areaFormat = string.Empty;
                string areaValue = string.Empty;
                string areaValuePromedio = string.Empty;

                foreach (var item in leaders)
                {
                    switch (area)
                    {
                        case "Puntos":
                            areaFormat = "Ptos";
                            areaValue = item.Ptos.ToString("0");
                            areaValuePromedio = item.PPtos.ToString("0.0");
                            break;

                        case "Rebotes":
                            areaFormat = "REB";
                            areaValue = item.Reb.ToString("0");
                            areaValuePromedio = item.PREB.ToString("0.0");
                            break;

                        case "Asistencias":
                            areaFormat = "AS";
                            areaValue = item.Asist.ToString("0");
                            areaValuePromedio = item.Prom.ToString("0.0");
                            break;
                        case "Tiros de campo":
                            areaFormat = "TC";
                            areaValue = $"{item.TCA.ToString("0")}/{item.TCI.ToString("0")}";
                            areaValuePromedio = item.PTC.ToString("0") + "%";
                            break;

                        case "Tiros de tres":
                            areaFormat = "T3";
                            areaValue = $"{item.T3A.ToString("0")}/{item.T3I.ToString("0")}";
                            areaValuePromedio = item.PT3.ToString("0") + "%";
                            break;
                        case "Tiros libres":
                            areaFormat = "TL";
                            areaValue = $"{item.TLA.ToString("0")}/{item.TLI.ToString("0")}";
                            areaValuePromedio = item.PTL.ToString("0") + "%";
                            break;
                        case "Robos":
                            areaFormat = "BR";
                            areaValue = item.BR.ToString("0");
                            areaValuePromedio = item.PBR.ToString("0.0");
                            break;
                        case "Perdidas":
                            areaFormat = "BP";
                            areaValue = item.BP.ToString("0");
                            areaValuePromedio = item.PBP.ToString("0.0");
                            break;
                        case "Minutos":
                            areaFormat = "MIN";
                            areaValue = item.Minutos.ToString("0.0");
                            areaValuePromedio = item.PMIN.ToString("0.0");
                            break;
                        case "Bloqueos":
                            areaFormat = "TB";
                            areaValue = item.TB.ToString("0");
                            areaValuePromedio = item.PTB.ToString("0.0");
                            break;
                        case "Faltas":
                            areaFormat = "FP";
                            areaValue = item.FP.ToString("0");
                            areaValuePromedio = item.PFP.ToString("0.0");
                            break;
                        default:
                            break;
                    };

                    //Lleno la lista a mostrar en la UI.
                    LeaderShort.Add(new LeaderShort
                    {
                        IDPlayer = item.IDPlayer,
                        NombrePlayer = item.NombreCompleto,
                        NombreEquipo = item.NombreEquipo,
                        Imagen = item.NombreImagen,
                        JJ = item.JJ,
                        Area = areaFormat,
                        AreaValor = areaValue,
                        AreaValorPromedio = areaValuePromedio
                    });
                }
            }

            IsBusy = false;
        }

        string SetAreaService(string area)
        {
            string newArea = string.Empty;
            switch (area)
            {
                case "Puntos":
                    newArea = "puntos";
                    break;

                case "Rebotes":
                    newArea = "rebotes";
                    break;

                case "Asistencias":
                    newArea = "asistencia";
                    break;
                case "Tiros de campo":
                    newArea = "tiroscampo";
                    break;
                case "Tiros de tres":
                    newArea = "tiros3";
                    break;
                case "Tiros libres":
                    newArea = "tiroslibres";
                    break;
                case "Robos":
                    newArea = "robadas";
                    break;
                case "Perdidas":
                    newArea = "perdidas";
                    break;
                case "Minutos":
                    newArea = "minutos";
                    break;
                case "Bloqueos":
                    newArea = "bloqueos";
                    break;
                case "Faltas":
                    newArea = "faltas";
                    break;
                default:
                    break;
            };

            return newArea;
        }

        string SetEtapaService(string etapa)
        {
            string newEtapa = string.Empty;
            switch (etapa)
            {
                case "S. Regular":
                    newEtapa = "SR";
                    break;

                case "SemiFinal":
                    newEtapa = "SF";
                    break;

                case "S. Final":
                    newEtapa = "FS";
                    break;                
                  
                default:
                    break;
            };

            return newEtapa;
        }

        public async void GetPublicidad()
        {
            List<Publicidad> Publicidad = new List<Publicidad>();

            var publicidadService = new PublicidadService();
            Publicidad = await publicidadService.GetPublicidad();

            foreach (var p in Publicidad)
            {
                PublicidadHTML = $"<html><body  style='text-align: center;'>" +
                    $"<img src='http://www.deportes56.com/Imagenes/Publicidad/{p.RutaPublicidad}' style='width:100%; height: 60px; border:1px solid gray'>" +
                    "</body></html>";
            }
        }
    }
}
