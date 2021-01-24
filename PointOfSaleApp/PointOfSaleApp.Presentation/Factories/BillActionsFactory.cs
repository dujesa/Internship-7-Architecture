using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.BillActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Factories
{
    public class BillActionsFactory
    {
        public static BillParentActions GetBillParentActions()
        {
            var actions = new List<IAction>
            {
                new IssueNewBillAction(RepositoryFactory.GetRepository<BillRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),

                new ExitMenuAction()
            };

            return new BillParentActions(actions);
        }
    }
}
