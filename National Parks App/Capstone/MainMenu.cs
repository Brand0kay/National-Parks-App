using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class MainMenu
    {
        // const string DatabaseConnectionString = @"";
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private const string CommandShowAllParks = "1";
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
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - Show all of the parks we work with");
            Console.WriteLine(" Q - Quit");
        }

        private void GetAllParks()
        {
        }
    }
}
