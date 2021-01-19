using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Extensions
{
    public static class ActionExtensions
    {
        public static void PrintActionsAndCall(this IList<IAction> actions)
        {
            var isExitSelected = false;

            while (!isExitSelected)
            {
                foreach (var action in actions)
                {
                    Console.WriteLine($"{action.MenuIndex} - {action.Label}");
                }

                var isInputValid = int.TryParse(Console.ReadLine(), out var actionIndex);

                if (!isInputValid)
                {
                    Console.WriteLine("Please type in number for action selection.");
                    //wrap in helper
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    var action = actions.FirstOrDefault(a => a.MenuIndex == actionIndex);

                    if (action == null)
                    {
                        Console.WriteLine("Please select available action by selecting valid action index.");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    else
                    {
                        isExitSelected = action is ExitMenuAction;
                        action.Call();
                    }
                }
            }
        }
        public static void SetActionIndexes(this IList<IAction> actions)
        {
            for (var index = 0; index < actions.Count; index++)
            {
                actions[index].MenuIndex = index + 1;
            }
        }
    }
}
