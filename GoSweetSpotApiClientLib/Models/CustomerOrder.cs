using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class CustomerOrder
    {
        public CustomerOrder()
        {

        }
        [JsonProperty("packingslipno")]

        public string OrderNumber { get; set; }
        public string Consignee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Delvref { get; set; }
        public string DelvInstructions { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string IconUrl { get; set; }
        public string CostCentre { get; set; }
        public string RawAddress { get; set; }
        //public RecordStatus status { get; set; }
        public List<Product> Products { get; set; }

        public class Product
        {
            [JsonProperty("productcode")]
            public string Code { get; set; }
            public string Description { get; set; }
            public decimal Units { get; set; }
            public decimal? UnitValue { get; set; }
            public string CountryofManufacture { get; set; }
            public decimal UnitKg { get; set; }
            public string ImageUrl { get; set; }
            public string Currency { get; set; }
            public decimal AlreadySent { get; set; }
            public decimal? FulfilledQty { get; set; }
        }
    }
}
