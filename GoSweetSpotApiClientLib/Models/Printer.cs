using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class PrintAgentPrinter
    {
        public string Printer { get; set; }
        public string IsLabelPrinter { get; set; }
        public bool IsOnline { get; set; }
        public string LabelType { get; set; }
        public string Name { get; set; }
        public string PrinterPath { get; set; }
        public string FullName { get; set; }
    }
}
