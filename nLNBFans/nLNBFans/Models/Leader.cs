using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class Leader
    {
        public int IDPlayer { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreImagen { get; set; }
        public string Condicion { get; set; }
        public int IDEquipo { get; set; }
        public string NombreEquipo { get; set; }
        public string Circuito { get; set; }
        public decimal JJ { get; set; }
        public decimal Ptos { get; set; }
        public decimal PPtos { get; set; }
        public decimal Reb { get; set; }
        public decimal PREB { get; set; }
        public decimal Asist { get; set; }
        public decimal Prom { get; set; }
        public decimal TCI { get; set; }
        public decimal TCA { get; set; }
        public decimal PTC { get; set; }
        public decimal T3I { get; set; }
        public decimal T3A { get; set; }
        public decimal PT3 { get; set; }
        public decimal TLI { get; set; }
        public decimal TLA { get; set; }
        public decimal PTL { get; set; }
        public decimal BR { get; set; }
        public decimal PBR { get; set; }
        public decimal TB { get; set; }
        public decimal PTB { get; set; }
        public decimal BP { get; set; }
        public decimal PBP { get; set; }
        public decimal JJE { get; set; }
        public decimal DO { get; set; }
        public decimal PDO { get; set; }
        public decimal Minutos { get; set; }
        public decimal PMIN { get; set; }
        public decimal FP { get; set; }
        public decimal PFP { get; set; }               
    }
}
