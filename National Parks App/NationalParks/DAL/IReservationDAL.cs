using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public interface IReservationDAL
    {
        bool IsReservationAvailable(int campNumber, string arrival, string departure);

        IList<Reservation> ViewAllReservations();
    }
}
