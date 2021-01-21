using PointOfSaleApp.Data.Entities.Models;
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
    public class ArticleEditAction : IAction
    {
        private readonly OfferRepository _offerRepository;
        private readonly ArticleRepository _articleRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit article";

        public ArticleEditAction(OfferRepository offerRepository, ArticleRepository articleRepository)
        {
            _offerRepository = offerRepository;
            _articleRepository = articleRepository;
        }

        public void Call()
        {
            var articles = _articleRepository.GetAll();
            ConsolePrinter.PrintArticles(articles);

            Console.WriteLine("\n Type article id(number) or any other key for exit");
            var isNumberInputted = ConsoleReader.IsNumberRead(out var articleId);
            if (!isNumberInputted)
                return;

            var article = articles.FirstOrDefault(a => a.Id == articleId);
            if (!(article is Article))
            {
                Console.WriteLine("Article was not found");
                ConsolePrinter.ClearScreenWithSleep();
            }

            var offer = article.Offer;

            Console.WriteLine("Press enter to skip editting displayed field.");

            Console.WriteLine($"Article name: [{offer.Name}]");
            offer.Name = ConsoleReader.IsLineRead(out var name) 
                ? name
                : offer.Name;

            Console.WriteLine($"Article description: [{offer.Description}]");
            offer.Description = ConsoleReader.IsLineRead(out var description)
                ? description
                : offer.Description;

            Console.WriteLine($"Article quantity available: [{offer.AvailableQuantity}]");
            offer.AvailableQuantity = ConsoleReader.IsNumberRead(out var quantity)
                ? quantity
                : offer.AvailableQuantity;

            Console.WriteLine($"Article price: [{article.Price}]");
            article.Price = ConsoleReader.IsDecimalRead(out var price)
                ? price
                : article.Price;

            var response = _articleRepository.Edit(article, articleId);

            Console.Clear();

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully edited article\n");
                ConsolePrinter.DisplayArticle(article);
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
