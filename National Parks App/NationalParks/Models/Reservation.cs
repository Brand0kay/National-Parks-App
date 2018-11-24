using System;
using System.Collections.Generic;
using System.Text;

namespace NationalParks.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int SiteId { get; set; }

        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime CreteDate { get; set; }
    }
}
