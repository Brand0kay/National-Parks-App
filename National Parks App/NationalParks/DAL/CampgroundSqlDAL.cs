using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public class CampgroundSqlDAL : ICampgroundDAL
    {
        private string connectionString;

        // construtor
        public CampgroundSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public IList<Campground> ViewSelectedParkCampgrounds(int parkId)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @parkId ", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = ConvertRowToCampground(reader);
                        output.Add(campground);
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

        private static Campground ConvertRowToCampground(SqlDataReader reader)
        {
            Campground camp = new Campground();
            camp.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            camp.ParkId = Convert.ToInt32(reader["park_id"]);
            camp.Name = Convert.ToString(reader["name"]);
            camp.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
            camp.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
            camp.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
            return camp;
        }
    }
}