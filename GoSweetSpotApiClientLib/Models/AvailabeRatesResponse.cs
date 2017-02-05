using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class AvailabeRatesResponse
    {
        public AvailabeRatesResponse()
        {
            Available = new List<AvailabeRate>();
            Rejected = new List<RejectDetail>();
            ValidationErrors = new Dictionary<string, string>();
        }
        public List<AvailabeRate> Available { get; set; }
        public List<RejectDetail> Rejected { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; }

        public class AvailabeRate
        {
            public Guid QuoteId { get; set; }
            public int CarrierId { get; set; }
            public string CarrierName { get; set; }
            public string DeliveryType { get; set; }
            public decimal Cost { get; set; }
            //public decimal Charge { get; set; }
            //public decimal Markup { get; set; }

            public string ServiceStandard { get; set; }
            public string Comments { get; set; }

            public string Route { get; set; }
            public bool IsRuralDelivery { get; set; }
            public bool IsSaturdayDelivery { get; set; }
            public bool IsFreightForward { get; set; }

            public string CarrierServiceType { get; set; }
        }

        public class RejectDetail
        {
            public string Reason { get; set; }
            public string Carrier { get; set; }
            public string DeliveryType { get; set; }
            //public int CarrierId { get; set; }
        }
    }
}
