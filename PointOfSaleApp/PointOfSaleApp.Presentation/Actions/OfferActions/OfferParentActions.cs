using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class OfferParentActions : BaseParentAction
    {
        public OfferParentActions(IList<IAction> actions) : base(actions)
        {
            Label = "Manage offers";
        }

        public override void Call()
        {
            Console.WriteLine("Offer managements");
            base.Call();
        }
    }
}
