using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.OfferActions;
using System.Collections.Generic;

namespace PointOfSaleApp.Presentation.Factories
{
    public class OfferActionsFactory
    {
        public static OfferParentActions GetOfferParentActions()
        {
            var actions = new List<IAction>
            {
                new ArticleAddAction(),
                new ExitMenuAction()
            };

            return new OfferParentActions(actions);
        }
    }
}
