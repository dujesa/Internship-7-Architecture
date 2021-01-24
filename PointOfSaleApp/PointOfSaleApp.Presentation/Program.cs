using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Authentificators;
using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.UserRepositories;
using PointOfSaleApp.Presentation.Extensions;
using PointOfSaleApp.Presentation.Factories;
using PointOfSaleApp.Presentation.Helpers;
using System;

namespace PointOfSaleApp.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---Point Of Sale App---");

            Employee employee;
            do
            {
                Console.WriteLine("Please input password to login or leave blank to exit POS app.");
                var inputPassword = Console.ReadLine();
                employee = AuthProvider.GetEmployee(inputPassword);

                if (String.IsNullOrEmpty(inputPassword))
                    break;

                if (employee == null)
                {
                    Console.WriteLine("\nWrong auth password, try again");
                    ConsolePrinter.ClearScreenWithSleep();
                }
            } while (!(employee is Employee));

            if (employee is Employee)
            {
                Console.WriteLine($"Succesfully logged in as {employee.FirstName} {employee.LastName}");
                var mainMenuActions = MainMenuFactory.GetMainMenuActions(employee);
                mainMenuActions.PrintActionsAndCall();
            }

            Console.Clear();
            Console.WriteLine("Thank you for using POS app.");
        }
    }
}
