using System.Collections.Generic;

namespace PointOfSaleApp.Presentation.Abstractions
{
    public interface IParentAction : IAction
    {
        IList<IAction> Actions { get; set; }
    }
}
