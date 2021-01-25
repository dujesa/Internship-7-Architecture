using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.ReportsActions
{
    public class ReportsParentActions : BaseParentAction
    {
        public ReportsParentActions(IList<IAction> actions) : base(actions)
        {
            Label = "Reports";
        }

        public override void Call()
        {
            Console.WriteLine("Reports");
            base.Call();
        }
    }
}
