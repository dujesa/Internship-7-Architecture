using PointOfSaleApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Helpers
{
    public static class ConsoleReader
    {
        public static bool IsExitReadOnNumberInput(out int number)
        {
            var isNumber = int.TryParse(Console.ReadLine(), out number);

            if (!isNumber)
            {
                Console.WriteLine("Exitting...");
                ConsolePrinter.ClearScreenWithSleep();

                number = 0;
            }

            return !isNumber;
        }

        public static (DateTime? StartTime, DateTime? EndTime) ProvideDatePeriod()
        {
            Console.WriteLine("Please input start time:");
            var isStartRead = IsDateTimeRead(out var startTime);

            Console.WriteLine("Please input end time:");
            var isEndRead = IsDateTimeRead(out var endTime);

            if (isStartRead && isEndRead)
                return (startTime, endTime);

            if (isStartRead)
                return (startTime, null);

            if (isEndRead)
                return (null, endTime);

            return (null, null);
        }

        public static bool IsDateTimeRead(out DateTime dateTime)
        {
            var isDateTime = DateTime.TryParse(Console.ReadLine(), out dateTime);

            return isDateTime;
        }

        public static bool IsNumberRead(out int number)
        {
            var isNumber = int.TryParse(Console.ReadLine(), out number);

            return isNumber;
        }

        public static bool IsDecimalRead(out decimal number)
        {
            var isNumber = decimal.TryParse(Console.ReadLine(), out number);

            return isNumber;
        }

        public static bool IsLineRead(out string readValue)
        {
            var inputValue = Console.ReadLine();

            if(string.IsNullOrEmpty(inputValue))
            {
                readValue = null;

                return false;
            }

            readValue = inputValue;

            return true;
        }

        public static bool ConfirmAction(string message) 
        {
            Console.WriteLine(message);
            Console.WriteLine("\n('yes' to confirm)");
            var isInputted = ConsoleReader.IsLineRead(out string input);

            return isInputted && (input.Equals("yes") || input.Equals("Yes"));
        }

        public static Customer ProvideCustomer()
        {
            var customer = new Customer();

            Console.Clear();
            Console.WriteLine($"-----Customer data form-----\n" +
                $"Enter subscriber first name:");
            customer.FirstName = Console.ReadLine();

            Console.WriteLine($"Enter subscriber last name:");
            customer.FirstName = Console.ReadLine();

            Console.WriteLine($"Enter subscriber PIN:");
            customer.PIN = Console.ReadLine();

            Console.WriteLine($"Enter subscriber credit card number:");
            customer.CreditCardNumber = Console.ReadLine();

            return customer;
        }
    }
}
