using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{

    public class AvailableServicesResponse
    {
        public Carrier[] Carriers { get; set; }
    }

    public class Carrier
    {
        public string CarrierName { get; set; }
        public string CarrierType { get; set; }
        public string[] Services { get; set; }
        public string AccountNumber { get; set; }
    }

}
