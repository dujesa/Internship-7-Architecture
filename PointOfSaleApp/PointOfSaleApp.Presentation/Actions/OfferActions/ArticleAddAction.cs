using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using System;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class ArticleAddAction : IAction
    {
        private readonly ArticleRepository _articleRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add new article";

        public ArticleAddAction(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void Call()
        {
            //toDo: Print option to choose offer category
            // 1. - repo get all
            // 2. - pretty print categories
            // 3. - get number for category

            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter description:");
            var description = Console.ReadLine();

            Console.WriteLine("Enter quantity available:");
            var isQuantityRead = false;
            var quantityAvailable = 0;
            while(!isQuantityRead)
            {
                isQuantityRead = int.TryParse(Console.ReadLine(), out quantityAvailable);
            }

            Console.WriteLine("Enter price:");
            var isPriceRead = false;
            var price = 0.0m;
            while (!isPriceRead)
            {
                isPriceRead = decimal.TryParse(Console.ReadLine(), out price);
            }

            //deconstruct or remove returns
            var result = _articleRepository.Add(name, description, quantityAvailable, price);

            Console.WriteLine(result.Message);

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
