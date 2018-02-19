using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class DeleteShipmentResponse
    {
        public List<Result> Results = new List<Result>();

        public class Result
        {
            public string ConsignmentNumber { get; set; }
            public string Message { get; set; }
        }
    }


}
