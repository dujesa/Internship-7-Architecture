using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.BillActions
{
    public class BillParentActions : BaseParentAction
    {
        public BillParentActions(IList<IAction> actions) : base(actions)
        {
            Label = "Manage bills";
        }

        public override void Call()
        {
            Console.WriteLine("Bill managements");
            base.Call();
        }
    }
}
