using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class RatesQueryOrShipmentRequest
    {
        public RatesQueryOrShipmentRequest()
        {
            ShipType = ShipTypeEnum.OUTBOUND;
        }
        
        public Contact Origin { get; set; }
        public Contact Destination { get; set; }
        public List<RatesPackage> Packages { get; set; }
        public List<Commodity> Commodities { get; set; }

        // Special Services
        public bool IsSaturdayDelivery { get; set; }
        public bool IsSignatureRequired { get; set; }
        public bool IsUrgentCouriers { get; set; }
        public bool DutiesAndTaxesByReceiver { get; set; }

        public bool RuralOverride { get; set; }
        public string DeliveryReference { get; set; }

        public string PrintToPrinter { get; set; }
        public Guid QuoteId { get; set; }

        public List<OutputEnum> Outputs { get; set; }
        
        public string Carrier { get; set; }
        public string Service { get; set; }

        public bool IncludeLineDetails { get; set; }

        public ShipTypeEnum ShipType { get; set; }

        public bool HasDG { get; set; }
        public DangerousGood DangerousGoods { get; set; }

        public bool DisableFreightForwardEmails { get; set; }

        public bool IncludeInsurance { get; set; }


        public enum ShipTypeEnum
        {
            OUTBOUND = 1,
            INBOUND = 2,
            THIRD_PARTY = 3
        }

        public class Contact
        {
            public Contact()
            {
                Id = 0;
            }
            public int Id { get; set; }

            public string Name { get; set; }

            public AddressModel Address { get; set; }

            public string Email { get; set; }

            public string ContactPerson { get; set; }

            public string PhoneNumber { get; set; }
            public bool IsRural { get; set; }
            public string DeliveryInstructions { get; set; }
            public bool SendTrackingEmail { get; set; }
            public int CostCentreId { get; set; }
            public bool ExplicitNotRural { get; set; }


            public class AddressModel
            {

                public string BuildingName { get; set; }

                public string StreetAddress { get; set; }

                public string Suburb { get; set; }

                public string City { get; set; }
                public string PostCode { get; set; }
                public string CountryCode { get; set; }
            }
        }
        public class RatesPackage
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Length { get; set; }
            public decimal Width { get; set; }
            public decimal Height { get; set; }
            public decimal Kg { get; set; }
            //public string Type { get; set; }

            public string PackageCode { get; set; }

            public enum PackageTypeEnum
            {
                Bag,
                Box,
                Carton,
                Container,
                Crate,
                Envelope,
                Pail,
                Pallet,
                Satchel,
                Tube
            }

            //public enum PackageCodeEnum
            //{
            //    CUSTOM = 1,
            //    DLE,
            //    A5,
            //    A4,
            //    A3,
            //    A2,

            //    DP,
            //    E11,
            //    E20,
            //    E25B,
            //    E40,
            //    E50,
            //    E60,
            //    PP
            //}


        }
        public class Commodity
        {
            public string HarmonizedCode { get; set; }
            public string Description { get; set; }
            public decimal UnitValue { get; set; }
            public int Units { get; set; }
            public decimal UnitKg { get; set; }
            public string Country { get; set; }
            public string Currency { get; set; }
        }
        public enum OutputEnum
        {
            LABEL_PNG = 1, // LEGACY
            LABEL_PDF = 2, // LEGACY
                           //COMMERCIAL_INVOICE = 3
            LABEL_PNG_100X175 = 4,
            LABEL_PNG_100X150 = 5,
            LABEL_PDF_100X175 = 6,
            LABEL_PDF_100X150 = 7,

            DG_FORM_PDF = 10,


            UNKNOWN = 0
        }



        public class DangerousGood
        {
            public string AdditionalHandlingInfo { get; set; }
            public string HazchemCode { get; set; }
            public bool IsRadioActive { get; set; }

            public bool CargoAircraftOnly { get; set; }

            public List<DangerousGoodItem> LineItems { get; set; }


            public class DangerousGoodItem
            {
                public int ConsignmentId { get; set; }

                public string Description { get; set; }

                public string ClassOrDivision { get; set; }

                public string UNorIDNo { get; set; }

                public string PackingGroup { get; set; }

                public string SubsidaryRisk { get; set; }

                public string Packing { get; set; }

                public string PackingInstr { get; set; }

                public string Authorization { get; set; }
            }
        }

    }
}
