using System;
using System.Collections.Generic;
using System.Text;

namespace NationalParks.Models
{
    public class Site
    {
        public int SiteID { get; set; }

        public int CampgroundId { get; set; }

        public int SiteNumber { get; set; }

        public int MaxOccupants { get; set; }

        public bool Accessible { get; set; }

        public int MaxRVLength { get; set; }

        public bool Utilities { get; set; }

        public override string ToString()
        {
            return $"{this.SiteID}\t\t{this.MaxOccupants}\t\t{this.Accessible}\t\t{this.MaxRVLength}\t\t\t{this.Utilities}";
        }
    }
}
