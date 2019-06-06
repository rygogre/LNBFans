using nLNBFans.Interfaces;
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
    public class News
    {
        NavigationService navigationService;
        public int IDNews { get; set; }
        public int IDColumnista { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime Fecha { get; set; }
        public string IDEquipos { get; set; }
        public string NombreImagen { get; set; }
        public string Estatus { get; set; }
        public string Fuente { get; set; }
        public string FechaPublicado { get; set; }

        #region Command
        /// <summary>
        /// Navegar al detalle de la noticia.
        /// </summary>
        public ICommand SelectCommand
        {           

           get {
                return new Command((e) => {
                    var n = (News)e;

                    navigationService.Navegate("NewsDetailPage", n);
                });
               }
        }


        /// <summary>
        /// Compartir redes sociales.
        /// </summary>
        public Command ShareCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var news = (News)e;
                    //DependencyService.Get<IShareSocial>().ShareSocial("Compartir", news.Titulo + "#LNBFans #playStore goo.gl/mYKel4  - #appStore goo.gl/KtnF09", $"http://deportes56.com/Imagenes/LNB/Detalles/{news.NombreImagen}", news.NombreImagen);
                     Helpers.SettingHelper.ShareUri(news.Titulo + " #LNBFans playStore goo.gl/mYKel4  - appStore goo.gl/KtnF09", "Compartir");
                   
                });
            }
        }
        #endregion

        public News()
        {
            navigationService = new NavigationService();

           
        }
    }
}
