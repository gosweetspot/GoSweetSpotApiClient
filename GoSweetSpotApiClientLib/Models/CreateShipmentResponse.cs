using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class CreateShipmentResponse
    {
        public CreateShipmentResponse()
        {
            //Connotes = new List<string>();
            Errors = new List<ErrorInfo>();
            Downloads = new List<string>();
        }
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }

        //public List<string> Connotes { get; set; }

        public bool IsFreightForward { get; set; }
        public bool IsOvernight { get; set; }
        public bool IsSaturdayDelivery { get; set; }
        public bool IsResidential { get; set; }
        public bool IsRural { get; set; }
        public bool HasTrackPaks { get; set; }
        //public bool PrintLabels { get; set; }

        public string Message { get; set; }


        public List<ErrorInfo> Errors { get; set; }

        public int SiteId { get; set; }

        public List<ConnoteSummary> Consignments { get; set; }

        public class ConnoteSummary
        {
            public string Connote { get; set; }
            public string TrackingUrl { get; set; }
            public decimal Cost { get; set; }
            public string CarrierType { get; set; }

            public bool IsSaturdayDelivery { get; set; }
            public bool IsRural { get; set; }
            public bool IsOvernight { get; set; }
            public bool IsResidential { get; set; }
            public bool HasTrackPaks { get; set; }

            public int ConsignmentId { get; set; }

            public Dictionary<string, List<byte[]>> OutputFiles { get; set; }
        }

        public List<string> Downloads { get; set; }
        public string CarrierType { get; set; }

        public string AlertPath { get; set; }

        public class ErrorInfo
        {
            public string Property { get; set; }
            public string Message { get; set; }

        }
    }

}
