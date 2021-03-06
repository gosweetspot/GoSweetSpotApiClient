﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoSweetSpotApiClientLib.Models.RatesQueryOrShipmentRequest;

namespace GoSweetSpotApiClientLib.Models
{

    public class AddressValidationResponse
    {
        public AddressValidationResponse()
        {
            Errors = new List<string>();
        }
        public AddressToValidate Address { get; set; }
        public bool Validated { get; set; }
        public List<string> Errors { get; set; }
    }

    public class AddressToValidate
    {
        public AddressToValidate()
        {
            AvailableServices = new List<AvailableService>();
        }

        public string Consignee { get; set; }
        public AddressObject Address { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryInstructions { get; set; }
        public List<AvailableService> AvailableServices { get; set; }

        public class AddressObject
        {
            public string BuildingName { get; set; }
            public string StreetAddress { get; set; }
            public string Suburb { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string CountryCode { get; set; }
            public bool IsRural { get; set; }
        }

        public class AvailableService
        {
            public string Carrier { get; set; }
            public bool IsResidential { get; set; }
            public bool IsRural { get; set; }
            public bool HasSaturdayService { get; set; }
            public string BranchCode { get; set; }
            public string RunNumber { get; set; }
        }
    }
}
