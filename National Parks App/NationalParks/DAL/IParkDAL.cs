using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public interface IParkDAL
    {
        IList<Park> GetAllParks();

        IList<Park> GetSpecificPark(int parkId);
    }
}
