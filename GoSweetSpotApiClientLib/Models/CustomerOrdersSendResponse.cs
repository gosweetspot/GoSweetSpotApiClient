using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class CustomerOrdersSendResponse
    {
        [JsonProperty("packingslipno")]
        public string OrderNumber { get; set; }
        public bool Success { get; set; }
        public string Msg { get; set; }
    }
}
