using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.DAL;
using NationalParks.Models;

namespace NationalParks
{
    public class SubMenu
    {
        // const string DatabaseConnectionString = @"";
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private const string CommandViewSelectedParkCampgrounds = "1";
        private const string CommandSearchReservations = "2";
        private const string CommandReturn = "3";
        private int pI = 0;

        public void Start()
        {
            Console.WriteLine($" Enter the Park number to view its campgrounds");
            string parkInput = Console.ReadLine();
            if (parkInput == "1" | parkInput == "2" | parkInput == "3")
            {
                this.pI = Convert.ToInt32(parkInput);
                int parkInt = Convert.ToInt32(parkInput);
                this.ShowSelectedPark(parkInt);
                this.Run();
            }
            else
            {
                Console.WriteLine("Please try again.");
            }
        }

        public void Run()
        {
            this.PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                switch (command.ToUpper())
                {
                    case CommandViewSelectedParkCampgrounds:
                        this.ViewSelectedParkCampgrounds(this.pI);
                        return;

                    case CommandSearchReservations:
                        this.SearchReservations(this.pI);
                        return;

                    case CommandReturn:
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }
                this.PrintMenu();
            }
        }

        private void SearchReservations(int parkId)
        {
            parkId = this.pI;
            this.ViewSelectedParkCampgrounds(this.pI);

            int campNumber = 0;

            Console.WriteLine("Which Campground (enter 0 to cancel)?__");
            campNumber = Convert.ToInt32(Console.ReadLine());
            if (campNumber == 0)
            {
                Console.Clear();
                this.PrintMenu();
            }
            else
            {
                Console.WriteLine("What is the Arrival Date? (mm/dd/yyyy) BE SURE TO USE SLASHES");
                string arrival = Console.ReadLine();
                DateTime arrivalDate = Convert.ToDateTime(arrival);

                Console.WriteLine("What is the Departure date? (mm/dd/yyyy) BE SURE TO USE SLASHES");

                string departure = Console.ReadLine();
                DateTime departureDate = Convert.ToDateTime(departure);

                this.SearchForReservationAvailability(campNumber, arrival, departure);
            }
        }

        private void ViewSelectedParkCampgrounds(int parkId)
        {
            parkId = this.pI;
            ICampgroundDAL campgroundDAL = new CampgroundSqlDAL(DatabaseConnectionString);

            IList<Campground> campgrounds = campgroundDAL.ViewSelectedParkCampgrounds(parkId);

            Console.WriteLine("\tOpen\t\tClose\t\tDaily Fee\tName\n");

            for (int index = 0; index < campgrounds.Count; index++)
            {
                Console.WriteLine(campgrounds[index]);
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine($"");
            Console.WriteLine("Park-Submenu: Type in a command");
            Console.WriteLine(" 1 - View Campgrounds");
            Console.WriteLine(" 2 - Search for reservations");
            Console.WriteLine(" 3 - Return to Main Menu");
        }

        private void ShowSelectedPark(int parkId)
        {
            IParkDAL parkDal = new ParkSqlDAL(DatabaseConnectionString);

            IList<Park> park = parkDal.GetSpecificPark(parkId);

            Console.WriteLine(park[0]);
        }

        private void SearchForReservationAvailability(int campNumber, string arrival, string departure)
        {
            IReservationDAL reservationDAL = new ReservationSqlDAL(DatabaseConnectionString);

            if (reservationDAL.IsReservationAvailable(campNumber, arrival, departure))
            {
                Console.WriteLine("Results matching your search Criteria:");

                ISiteDAL site = new SiteSqlDAL(DatabaseConnectionString);

                IList<Site> sites = site.GetAvailableSitesFromCampground(campNumber);


                Console.WriteLine($"Site No.\tMax Occup.\tAccessible?\tMax RV Length\t\tUtility\tCost\n");

                for (int index = 0; index < sites.Count; index++)
                {
                    Console.WriteLine(sites[index]);
                }



            }
            else Console.WriteLine("Date already selected. Try Again.");
        }
    }
}
