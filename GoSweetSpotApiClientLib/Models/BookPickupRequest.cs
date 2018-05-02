using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class BookPickupRequest
    {
        public string Carrier { get; set; }
        public List<string> Consignments { get; set; }
        public decimal? TotalKg { get; set; }
        public int? Parts { get; set; }
    }
}
