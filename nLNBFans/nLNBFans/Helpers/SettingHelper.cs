using nLNBFans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using System.Net;
using System.IO;
using Xamarin.Essentials;

namespace nLNBFans.Helpers
{
    public static class SettingHelper
    {
        public static bool ConnectionDevice()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;            
        }

        public static List<string> Temporadas()
        {
            int firstYear = 2012;
            int thisYear = DateTime.Now.Year;
            List<string> years = new List<string>();

            int totalYears = thisYear - firstYear;

            for (int i = 0; i <= totalYears; i++)
            {
                int year = thisYear - i;
                years.Add((year.ToString()));
            }

            return years;
        }

        public async static void TemporadaActual()
        {           
            var temporadaActual = new TemporadaActual();
            Services.SettingService settingService = new Services.SettingService();
            temporadaActual = await settingService.GetTemporadaActual();

            if (temporadaActual.Temporada.Length > 0)
            {
                Preferences.Set("temporadaActual", temporadaActual.Temporada);
                Preferences.Set("etapa", temporadaActual.Etapa);
            }
        }

       
        public static void CreateTags()
        {            
            var tags = Preferences.Get("appTags", string.Empty);

            if (tags.ToString().Equals(string.Empty))
                Preferences.Set("appTags", "0,1,2,4,5");
        }


        public static async Task ShareText(string text)
        {
            await DataTransfer.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        public static void ShareUri(string uri, string title)
        {
            DataTransfer.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }
        




        //public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        //{
        //    // get hold of the file system  
        //    IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //    ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
        //    // already run at least once, don't overwrite what's there  
        //    if (folderexist == ExistenceCheckResult.FileExists)
        //    {
        //        return true;

        //    }
        //    return false;
        //}

        //public async static Task<bool> IsFolderExistAsync(this string folderName, IFolder rootFolder = null)
        //{
        //    // get hold of the file system  
        //    IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //    ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
        //    // already run at least once, don't overwrite what's there  
        //    if (folderexist == ExistenceCheckResult.FolderExists)
        //    {
        //        return true;

        //    }
        //    return false;
        //}

        //public async static Task<IFolder> CreateFolder(this string folderName, IFolder rootFolder = null)
        //{
        //    IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //    folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
        //    return folder;
        //}

        //public async static Task<IFile> CreateFile(this string filename, IFolder rootFolder = null)
        //{
        //    IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //    IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
        //    return file;
        //}

        //public async static Task<bool> WriteTextAllAsync(this string filename, string content = "", IFolder rootFolder = null)
        //{
        //    IFile file = await filename.CreateFile(rootFolder);
        //    await file.WriteAllTextAsync(content);
        //    return true;
        //}

        //public async static Task<string> ReadAllTextAsync(this string fileName, IFolder rootFolder = null)
        //{
        //    string content = "";
        //    IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //    bool exist = await fileName.IsFileExistAsync(folder);
        //    if (exist == true)
        //    {
        //        IFile file = await folder.GetFileAsync(fileName);
        //        content = await file.ReadAllTextAsync();
        //    }
        //    return content;
        //}


        /// <summary>
        /// Permite crear el archivo de tabs y agrega tabs iniciales.
        /// </summary>
        //public async static void CreateTagFile()
        //{
        //    string tags = "0,1,2,3,4,5";
        //    bool file = await Helpers.SettingHelper.IsFileExistAsync("tagsLNBFans.txt");
        //    if (!file)
        //    {
        //        await Helpers.SettingHelper.CreateFile("tagsLNBFans.txt");
        //        try
        //        {
        //            await Helpers.SettingHelper.WriteTextAllAsync("tagsLNBFans.txt", tags);
        //        }
        //        catch { }
        //    }
        //}



        //public async static Task<string[]> GetTag()
        //{
        //    string rTags = await Helpers.SettingHelper.ReadAllTextAsync("tagsLNBFans.txt");
        //    //if (string.IsNullOrEmpty(rTags))
        //    //  CreateTagFile();

        //    string[] tags = rTags.Split(',');


        //    return tags;
        //}

        public static string TeamFiltered(string team)
        {
            switch (team)
            {
                case "Todos":
                    team = "t";
                    break;

                case "Circuito Norte":
                    team = "n";
                    break;
                case "Indios":
                    team = "1";
                    break;
                case "Reales":
                    team = "2";
                    break;
                case "Metros":
                    team = "3";
                    break;
                case "Huracanes":
                    team = "4";
                    break;
                case "Circuito Suroeste":
                    team = "s";
                    break;
                case "Leones":
                    team = "5";
                    break;
                case "Titanes":
                    team = "6";
                    break;
                case "Cañeros":
                    team = "8";
                    break;
                case "Soles":
                    team = "25";
                    break;

                default:
                    break;
            }

            return team;
        }

        public static string SaltoLinea(string str)
        {
            str = str.Replace(Convert.ToChar(10), '*');
            str = str.Replace("*", "<br />");

            return str;
        }
    }
}
