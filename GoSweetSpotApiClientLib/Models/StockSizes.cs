using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class StockSizesRequest
    {
        public int? PackageStockId { get; set; }
        public string Name  { get; set; }
        public decimal Height  { get; set; }
        public decimal Length  { get; set; }
        public decimal Width  { get; set; }
        public string Type  { get; set; }
        public decimal Weight  { get; set; }
        public int Sort  { get; set; }
        public bool IsTrackPak  { get; set; }
        public bool HeightAdjustable  { get; set; }

        public AvailabilityEnum Availability  { get; set; }

        public enum AvailabilityEnum
        {
            Me_Only = 1,
            This_Site_Only = 2,
            Entire_Group = 3
        }
    }
    public class StockSizesResponse
    {
        public int PackageStockId { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Cubic { get; set; }
        public string Type { get; set; }
        public decimal Weight { get; set; }
        public int Sort { get; set; }
        public bool IsTrackPak { get; set; }
        public bool HeightAdjustable { get; set; }
        public string Availability { get; set; }
    }
}
