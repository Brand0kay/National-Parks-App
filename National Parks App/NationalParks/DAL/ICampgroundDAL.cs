using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public interface ICampgroundDAL
    {
        IList<Campground> ViewSelectedParkCampgrounds(int parkId);

    }
}
