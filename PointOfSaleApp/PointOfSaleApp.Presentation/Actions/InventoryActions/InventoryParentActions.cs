using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.InventoryActions
{
    public class InventoryParentActions : BaseParentAction
    {
        public InventoryParentActions(IList<IAction> actions) : base(actions)
        {
            Label = "Manage inventory";
            Actions = actions;
        }

        public override void Call()
        {
            Console.WriteLine("Inventory managements");
            base.Call();
        }
    }
}
