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

    }
}
