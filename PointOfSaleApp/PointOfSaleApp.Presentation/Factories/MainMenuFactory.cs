using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Extensions;
using System.Collections.Generic;

namespace PointOfSaleApp.Presentation.Factories
{
    public static class MainMenuFactory
    {
        public static IList<IAction> GetMainMenuActions(Employee employee)
        {
            var actions = new List<IAction>
            {
                OfferActionsFactory.GetOfferParentActions(),
                OfferCategoryActionsFactory.GetOfferCategoryParentActions(),
                InventoryActionsFactory.GetInventoryParentActions(),
                BillActionsFactory.GetBillParentActions(employee),
                ReportsActionsFactory.GetReportsParentActions(),

                new ExitMenuAction()
            };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
