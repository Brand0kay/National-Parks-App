using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using NationalParks.Models;

namespace NationalParks.DAL
{
    public class ReservationSqlDAL : IReservationDAL
    {
        private string connectionString;


        // construtor
        public ReservationSqlDAL(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public IList<Reservation> ViewAllReservations()
        {
            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = this.ConvertRowToReservation(reader);
                        output.Add(reservation);
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

        public bool IsReservationAvailable(int campNumber, string arrival, string departure)
        {
            List<Reservation> output = new List<Reservation>();
            bool isAvailable = false;
            try
            {
                int count = 0;
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
                        Reservation reservation = this.ConvertRowToReservation(reader);

                        output.Add(reservation);
                        count++;
                    }

                    if (count == 0)
                    {
                        isAvailable = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error reading park data.");
                throw;
            }

            return isAvailable;
        }

        private Reservation ConvertRowToReservation(SqlDataReader reader)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
            reservation.SiteId = Convert.ToInt32(reader["site_id"]);
            reservation.Name = Convert.ToString(reader["name"]);
            reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
            reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
            reservation.CreteDate = Convert.ToDateTime(reader["create_date"]);

            return reservation;
        }
    }
}
