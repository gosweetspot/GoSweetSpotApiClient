using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class ShipmentStatusFull
    {
        public ShipmentStatusFull()
        {
            Events = new List<Event>();
        }
        public string ConsignmentNo { get; set; }
        public string Status { get; set; }
        public DateTime? Picked { get; set; }
        public DateTime? Delivered { get; set; }
        public string Tracking { get; set; }

        public List<Event> Events { get; set; }

        public string ManifestNumber { get; set; }

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
        public string OriginZone { get; set; }
        public string DestinationZone { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }

        public class Location
        {
            public string Building { get; set; }
            public string Address { get; set; }
            public string Name { get; set; }
            public string Suburb { get; set; }
            public string Town { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string ContactName { get; set; }
            public string ContactPhone { get; set; }
            public string Email { get; set; }
        }

        public string CostCentre { get; set; }

        public string Carrier { get; set; }

        public string DeliveryInstructions { get; set; }

        public bool IsSaturdayDelivery { get; set; }
        public bool IsRuralDelivery { get; set; }
        public bool IsPOBox { get; set; }

        public string CustomerRef { get; set; }

        public decimal TotalCubic { get; set; }
        public decimal TotalKg { get; set; }
        public int Parts { get; set; }

        public bool IsSignatureRequired { get; set; }
        public bool IsFreightForward { get; set; }

        public List<ConsignmentItem> Items { get; set; }

        public class ConsignmentItem
        {
            public int PartNo { get; set; }
            public decimal LengthCm { get; set; }
            public decimal WidthCm { get; set; }
            public decimal HeightCm { get; set; }
            public decimal WeightKg { get; set; }
            public string PackageName { get; set; }
            public string PackageStockCode { get; set; }
            public decimal Charge_LineTotal { get; set; }
            public DateTime? PickedAt { get; set; }
            public DateTime? DeliveredAt { get; set; }
        }
    }
}
