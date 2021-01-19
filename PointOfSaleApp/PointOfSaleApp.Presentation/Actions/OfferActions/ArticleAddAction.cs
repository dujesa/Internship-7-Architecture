using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class ArticleAddAction : IAction
    {
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add new article";

        public void Call()
        {
            Console.WriteLine("ToDo - implement add article");
        }
    }
}
