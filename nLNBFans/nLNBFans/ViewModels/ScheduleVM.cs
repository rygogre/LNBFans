using nLNBFans.Helpers;
using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class ScheduleVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private List<Schedule> schedule;
        private List<Schedule> scheduleTeam;
        private string publicidadHTML;

        public List<Schedule> Schedule
        {
            get { return schedule; }
            set { schedule = value; OnPropertyChanged(); }
        }

        public List<Schedule> ScheduleTeam
        {
            get { return scheduleTeam; }
            set { scheduleTeam = value; OnPropertyChanged(); }
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

        private ObservableCollection<ObservableGroupCollection<string, Schedule>> scheduleGrouped;

        public ObservableCollection<ObservableGroupCollection<string, Schedule>> ScheduleGrouped
        {
            get { return scheduleGrouped; }
            set
            {
                scheduleGrouped = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public ScheduleVM()
        //{
        //    GetSchedule(Helpers.Settings.TemporadaSettings, Helpers.Settings.EtapaSettings);
        //}

        public ScheduleVM(int idTeam,string year, string etapa)
        {
            GetSchedule(idTeam, year, etapa);
            GetPublicidad();
        }

        public ScheduleVM(int idTeam)
        {
            string temporada = "2018"; //Helpers.Settings.TemporadaSettings.Equals(string.Empty) ? "2018" : Helpers.Settings.TemporadaSettings;
            string etapa = "SR"; //Helpers.Settings.EtapaSettings.Equals(string.Empty) ? "SR" : Helpers.Settings.EtapaSettings;

            GetScheduleTeam(idTeam,temporada,etapa);
        }

        public async void GetSchedule(int idTeam, string year, string etapa)
        {
            IsBusy = true;
            Schedule = new List<Schedule>();

            var scheduleService = new ScheduleService();
            Schedule = await scheduleService.GetSchedule(year, etapa);
            
            //Agrego un IF?, para filtrar cuando viene por un EQUIPO....
            var sorted = idTeam == 0 ? from schedule in Schedule
                                       orderby schedule.Fecha
                                       group schedule by schedule.FechaNueva into scheduleGroup
                                       select new ObservableGroupCollection<string, Schedule>(scheduleGroup.Key.ToString(), scheduleGroup)
                         :
                         from schedule in Schedule
                         where schedule.IDEquipoCasa == idTeam || schedule.IDEquipoVisita == idTeam
                         orderby schedule.Fecha
                         group schedule by schedule.FechaNueva into scheduleGroup
                         select new ObservableGroupCollection<string, Schedule>(scheduleGroup.Key.ToString(), scheduleGroup);

            //create a new collection of groups
            ScheduleGrouped = new ObservableCollection<ObservableGroupCollection<string, Schedule>>(sorted);


            IsBusy = false;
        }

        public async void GetScheduleTeam(int idTEam, string year, string etapa)
        {
            IsBusy = true;
            ScheduleTeam = new List<Schedule>();

            var scheduleService = new ScheduleService();
            Schedule = await scheduleService.GetSchedule(year, etapa);

            var query = from schedule in Schedule
                         where schedule.IDEquipoCasa == idTEam || schedule.IDEquipoVisita == idTEam
                         select schedule;
            if (query.Count() > 0)
                ScheduleTeam = query.ToList();

            IsBusy = false;
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
