using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class GameScoreTime
    {
        public int ID { get; set; }
        public int IDCalendario { get; set; }
        public int PVisita { get; set; }
        public int SVisita { get; set; }
        public int TVisita { get; set; }
        public int CVisita { get; set; }
        public int TEVisita { get; set; }
        public int PCasa { get; set; }
        public int SCasa { get; set; }
        public int TCasa { get; set; }
        public int CCasa { get; set; }
        public int TECasa { get; set; }
        [JsonIgnore]
        public int FinalVisita {
            get { return PVisita + SVisita + TVisita + CVisita + TEVisita; } }
        [JsonIgnore]
        public int FinalCasa { get { return PCasa + SCasa + TCasa + CCasa + TECasa; } }

    }
}
