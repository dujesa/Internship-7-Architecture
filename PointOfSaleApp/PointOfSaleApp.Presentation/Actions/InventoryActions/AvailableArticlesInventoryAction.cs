using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.InventoryActions
{
    public class AvailableArticlesInventoryAction : IAction
    {
        private readonly ArticleRepository _articleRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Review article inventory";

        public AvailableArticlesInventoryAction(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void Call()
        {
            Console.Clear();

            var availableArticles = _articleRepository.GetAvailable();

            Console.WriteLine($"List of all available articles in inventory:");
            ConsolePrinter.PrintArticles(availableArticles);
        }
    }
}
