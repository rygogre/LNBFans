using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class TeamInformation
    {
        public int IDStaff { get; set; }
        public int IDEquipo { get; set; }
        public string GerenteGeneral { get; set; }
        public string Dirigente { get; set; }
        public string PrimerAsistente { get; set; }
        public string SegundoAsistente { get; set; }
        public string PresidenteEquipo { get; set; }
        public string Administrador { get; set; }
        public string DirectorPrensa { get; set; }
        public string DirectorMarketing { get; set; }
    }
}
