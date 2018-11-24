using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NationalParks.Models
{
    public class Park
    {
        public int ParkId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime EstDate { get; set; }

        public int Area { get; set; }

        public int Visitors { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{this.Name}\nLocation:\t\t{this.Location}\nEstablished:\t\t{this.EstDate.ToString("d")}\nArea:\t\t\t{this.Area.ToString("N0")}sq km\nAnnual Visitors:\t{this.Visitors.ToString("N0")}\n{this.Description}\n";
        }
    }
}
