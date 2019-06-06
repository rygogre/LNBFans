using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Interfaces
{
    public interface IShareSocial
    {
        void ShareSocial(string subject, string message, string imageURL, string imageName);
    }
}
