using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class ShipmentStatus
    {
        public ShipmentStatus()
        {
            Events = new List<Event>();
        }
        public string ConsignmentNo { get; set; }
        public string Status { get; set; }
        public DateTime? Picked { get; set; }
        public DateTime? Delivered { get; set; }
        public string Tracking { get; set; }

        public List<Event> Events { get; set; }


        public class Event
        {
            public DateTime EventDT { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }

            public string Part { get; set; }
        }

        public decimal TotalCost { get; set; }

        public DateTime CreatedUtc { get; set; }

        public string PackingSlipNo { get; set; }

        public bool? ManualTicket { get; set; }

        public string Consignee { get; set; }
    }
}
