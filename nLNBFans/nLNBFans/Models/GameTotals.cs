using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class GameTotals
    {
        public int IDEquipo { get; set; }
        public Double Asist { get; set; }
        public Double ROF { get; set; }
        public Double DEF { get; set; }
        public Double TR { get; set; }
        public Double TCI { get; set; }
        public Double TCA { get; set; }
        public Double PCTTC { get; set; }
        public Double T3I { get; set; }
        public Double T3A { get; set; }
        public Double PCTT3 { get; set; }
        public Double TLI { get; set; }
        public Double TLA { get; set; }
        public Double PCTTL { get; set; }
        [JsonIgnore]
        public string TC { get { return $"{TCA.ToString()}/{TCI.ToString()}"; } }
        [JsonIgnore]
        public string T3 { get { return $"{T3A.ToString()}/{T3I.ToString()}"; } }
        [JsonIgnore]
        public string TL { get { return $"{TLA.ToString()}/{TLI.ToString()}"; } }
    }
}
