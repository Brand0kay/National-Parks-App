using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public class SiteSqlDAL : ISiteDAL
    {
        private string connectionString;

        // construtor
        public SiteSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public IList<Site> GetAvailableSitesFromCampground(int campgroundCode)
        {
            List<Site> output = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM site inner join campground on site.campground_id = campground.campground_id WHERE campground.campground_id = @campgroundCode", conn);

                    cmd.Parameters.AddWithValue("@campgroundCode", campgroundCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site site = this.ConvertRowToSite(reader);

                        output.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error reading campground data.");
                throw;
            }

            return output;
        }

        public IList<Site> GetSitesMatchingCriteria(int campNumber, string arrival, string departure)
        {
            List<Site> output = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation inner join site on reservation.site_id=site.site_id inner join campground on site.campground_id=campground.campground_id where reservation.from_date between @arrival and @departure and reservation.to_date between @arrival and @departure and campground.campground_id = @campNumber", conn);

                    cmd.Parameters.AddWithValue("@arrival", arrival);
                    cmd.Parameters.AddWithValue("@campNumber", campNumber);
                    cmd.Parameters.AddWithValue("@departure", departure);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site site = this.ConvertRowToSite(reader);

                        output.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error reading park data.");
                throw;
            }

            return output;
        }

        private Site ConvertRowToSite(SqlDataReader reader)
        {
            Site site = new Site();
            site.SiteID = Convert.ToInt32(reader["site_id"]);
            site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
            site.MaxOccupants = Convert.ToInt32(reader["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(reader["accessible"]);
            site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(reader["utilities"]);
            return site;
        }
    }
}
