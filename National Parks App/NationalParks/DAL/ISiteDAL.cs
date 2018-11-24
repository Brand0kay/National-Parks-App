using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public interface ISiteDAL
    {
        IList<Site> GetSitesMatchingCriteria(int campNumber, string arrival, string departure);

        IList<Site> GetAvailableSitesFromCampground(int campgroundCode);

    }
}
