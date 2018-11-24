using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private string connectionString;

        // construtor
        public ParkSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public IList<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertRowToPark(reader);
                        output.Add(park);
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

        IList<Park> IParkDAL.GetSpecificPark(int parkId)
        {
            List<Park> output = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE park_id = @parkId", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertRowToPark(reader);
                        output.Add(park);
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

        private static Park ConvertRowToPark(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkId = Convert.ToInt32(reader["park_id"]);
            park.Name = Convert.ToString(reader["name"]);
            park.Location = Convert.ToString(reader["location"]);
            park.EstDate = Convert.ToDateTime(reader["establish_date"]);
            park.Area = Convert.ToInt32(reader["area"]);
            park.Visitors = Convert.ToInt32(reader["visitors"]);
            park.Description = Convert.ToString(reader["description"]);
            return park;
        }
    }
}
