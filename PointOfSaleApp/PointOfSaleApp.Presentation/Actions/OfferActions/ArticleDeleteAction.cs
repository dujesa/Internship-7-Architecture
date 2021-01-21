using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class ArticleDeleteAction : IAction
    {
        private readonly ArticleRepository _articleRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete article";

        public ArticleDeleteAction(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void Call()
        {
            var articles = _articleRepository.GetAll();
            ConsolePrinter.ShortPrintArticles(articles);

            Console.WriteLine("\n Type article id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var articleId);
            if (isExitInputted)
                return;

            var response = _articleRepository.Delete(articleId);

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully deleted article\n");
            }

            if (response == ResponseResultType.NotFound)
            {
                Console.WriteLine("Article not found");
            }

            if (response == ResponseResultType.NoChanges)
            {
                Console.WriteLine("No changes applied");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
