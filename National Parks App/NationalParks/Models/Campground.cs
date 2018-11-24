using System;
using System.Collections.Generic;
using System.Text;

namespace NationalParks.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }

        public int ParkId { get; set; }

        public string Name { get; set; }

        public int OpenFrom { get; set; }

        public int OpenTo { get; set; }

        public decimal DailyFee { get; set; }

        public override string ToString()
        {

            Dictionary<int, string> month = new Dictionary<int, string>()
            {
                { 1, "January" },
                { 2, "February" },
                { 3, "March" },
                { 4, "April" },
                { 5, "May" },
                { 6, "June" },
                { 7, "July" },
                { 8, "August" },
                { 9, "September" },
                { 10, "October   " },
                { 11, "November" },
                { 12, "December" },
            };

            return $"#{this.CampgroundId}\t{month[this.OpenFrom]}\t\t{month[this.OpenTo]}\t{this.DailyFee.ToString("N2")}\t\t{this.Name}\n";
        }
    }
}
