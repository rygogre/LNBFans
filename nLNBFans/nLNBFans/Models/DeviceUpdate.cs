using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Models
{
    public class DeviceUpdate
    {
        public string Handler { get; set; }
        public string ID { get; set; }
        public string Platform { get; set; }
        public string[] Tags { get; set; }        
    }
}
