using PointOfSaleApp.Presentation.Extensions;
using PointOfSaleApp.Presentation.Factories;
using System;

namespace PointOfSaleApp.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainMenuActions = MainMenuFactory.GetMainMenuActions();
            mainMenuActions.PrintActionsAndCall();
        }
    }
}
