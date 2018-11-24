using System;
using System.Collections.Generic;
using System.Text;
using NationalParks.DAL;
using NationalParks.Models;

namespace NationalParks
{
    public class MainMenu
    {
        // const string DatabaseConnectionString = @"";
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private const string CommandShowAllParks = "1";
        private const string CommandShowParkSubMenu = "2";
        private const string CommandQuit = "Q";

        public void Run()
        {
            this.PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToUpper())
                {
                    case CommandShowAllParks:
                        this.GetAllParks();
                        break;

                    case CommandShowParkSubMenu:
                        this.ShowParkSubMenu();
                        break;

                    case CommandQuit:
                        Console.WriteLine("Thank you for using our Park reservation system");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

                this.PrintMenu();
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Main-Menu: Type in a command");
            Console.WriteLine(" 1) - Show all of the parks we work with");
            Console.WriteLine(" 2) - Select a specific park submenu");
            Console.WriteLine(" Q) - Quit");
        }

        private void GetAllParks()
        {
            IParkDAL parkDal = new ParkSqlDAL(DatabaseConnectionString);

            IList<Park> parks = parkDal.GetAllParks();

            Console.WriteLine();
            Console.WriteLine("The Parks are:\n");

            for (int index = 0; index < parks.Count; index++)
            {
                Console.WriteLine(index + " - " + parks[index]);
            }
        }

        private void ShowParkSubMenu()
        {
            SubMenu subMenu = new SubMenu();
            Console.Clear();
            subMenu.Start();
        }
    }
}