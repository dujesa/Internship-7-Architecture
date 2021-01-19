using PointOfSaleApp.Presentation.Abstractions;
using System;

namespace PointOfSaleApp.Presentation.Actions
{
    public class ExitMenuAction : IAction
    {
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Exit menu";

        public ExitMenuAction()
        {
        }

        public void Call()
        {
            Console.Clear();
        }
    }
}
