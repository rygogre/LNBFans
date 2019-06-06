using nLNBFans.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.Models
{
    public class Team
    {
        NavigationService navigationService;
        public int IDEquipo { get; set; }
        public string NombreEquipo { get; set; }
        public string LogoEquipo { get; set; }
        public string Color { get; set; }
        public string Circuito { get; set; }

        public ICommand SelectCommand
        {
            get
            {
                return new Command(() =>
                {
                    navigationService.Navegate("TeamDetailPage", IDEquipo, NombreEquipo);
                });
            }
        }

        public Team()
        {
            navigationService = new NavigationService();
        }
    }
}
