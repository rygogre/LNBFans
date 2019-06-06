using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//using Xamarin.Forms.Extended;

namespace nLNBFans.ViewModels
{
    public class NewsVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private const int PageSize = 5;
        //private InfiniteScrollCollection<News> news;
        private List<News> newsAll;
        private string publicidadHTML;

        //public InfiniteScrollCollection<News> News
        //{
        //    get { return news; }
        //    set { news = value; OnPropertyChanged(); }
        //}

        public List<News> NewsAll
        {
            get { return newsAll; }
            set { newsAll = value; OnPropertyChanged(); }
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

        //Constructor.....
        public NewsVM(int pageIndex)
        {
            GetNews();
            //News = new InfiniteScrollCollection<News>
            //{
            //    OnLoadMore = async () =>
            //    {
            //        IsBusy = true;
            //        // load the next page
            //        var page = News.Count / PageSize;
            //        var news = await GetNews(page);
            //        IsBusy = false;

            //        // return the items that need to be added
            //        return news;
            //    },
            //    OnCanLoadMore = () =>
            //    {
            //        return News.Count < newsAll.Count;
            //    }
            //};

            ////Primera pagina....
            //var x = GetNews(page: 0);
            GetPublicidad();
        }



        //public async Task<List<News>> GetNews(int page)
        //{
        //    IsBusy = true;

        //    //if (newsAll.Count == 0)
        //    //{
        //    //    NewsAll = new List<News>();

        //    //    var newsService = new NewsService();
        //    //    NewsAll = await newsService.GetNews();


        //    //    News.AddRange(NewsAll.Skip(page * 5).Take(5).ToList());
        //    //}
        //    //else
        //    //    await Task.Delay(1000); //Leve daleay como TEST

        //    //return newsAll.Skip(page * 5).Take(5).ToList();

        //    NewsAll = new List<News>();


        //    IsBusy = false;

        //    newsAll = null;
        //}


        public async void GetNews()
        {
            NewsAll = new List<News>();

            var newsService = new NewsService();
            NewsAll = await newsService.GetNews();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
