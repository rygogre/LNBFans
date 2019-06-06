using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class TemporadaActual
    {
        public string Temporada { get; set; }
        public string Etapa { get; set; }
        [JsonIgnore]
        public string TemporadaEtapa { get { return $"{Temporada};{Etapa}"; } }
    }
}
