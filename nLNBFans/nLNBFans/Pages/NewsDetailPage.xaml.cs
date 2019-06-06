using nLNBFans.Interfaces;
using nLNBFans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nLNBFans.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsDetailPage : ContentPage
	{
        string textDetail = string.Empty;
        string urlDetail = string.Empty;
        string imageNombre = string.Empty;

        public NewsDetailPage (object news)
		{
			InitializeComponent ();

            var newDetail = (News)news;
            textDetail = newDetail.Titulo;
           
            imageNombre = newDetail.NombreImagen;


            BindingContext = newDetail;
            descripcionWebView.VerticalOptions = LayoutOptions.FillAndExpand;

            var text = $"<html><body  style='text-align: justify;'>" +
                    $"<img src='http://www.deportes56.com/Imagenes/LNB/Detalles/{newDetail.NombreImagen}' style='width:100%; height: 160px;'>" +
                    $"<div style='text-align: right; font-size:11px; font-color:Gray;'>{newDetail.FechaPublicado} * <strong>{newDetail.Fuente}</strong></div>" +
                    $"<h3>{newDetail.Titulo}</h3>" +
                    String.Format("<p>{0}</p>", Helpers.SettingHelper.SaltoLinea(newDetail.Descripcion)) +
                    "</body>" +
                    "</html>";

            descripcionWebView.Source = new HtmlWebViewSource { Html = text };
        }

        public void OnShareClicked(object sender, EventArgs args)
        {
            //DependencyService.Get<IShareSocial>().ShareSocial("Compartir", textDetail + " #LNBFans " + "#playStore goo.gl/mYKel4  - #appStore goo.gl/KtnF09", $"http://deportes56.com/Imagenes/LNB/Detalles/{imageNombre}", imageNombre);
            Helpers.SettingHelper.ShareUri(textDetail + " #LNBFans playStore goo.gl/mYKel4  - appStore goo.gl/KtnF09", "Compartir");

        }
    }
}