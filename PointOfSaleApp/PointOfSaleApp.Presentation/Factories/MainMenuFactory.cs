using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Extensions;
using System.Collections.Generic;

namespace PointOfSaleApp.Presentation.Factories
{
    public static class MainMenuFactory
    {
        public static IList<IAction> GetMainMenuActions()
        {
            var actions = new List<IAction>
            {
                OfferActionsFactory.GetOfferParentActions(),
                new ExitMenuAction()
            };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
