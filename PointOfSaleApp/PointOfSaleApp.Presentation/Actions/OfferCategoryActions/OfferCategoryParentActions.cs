using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferCategoryActions
{
    public class OfferCategoryParentActions : BaseParentAction
    {
        public OfferCategoryParentActions(IList<IAction> actions) : base(actions)
        {
            Label = "Manage offer categories";
        }

        public override void Call()
        {
            Console.WriteLine("Offer category managements");
            base.Call();
        }
    }
}
