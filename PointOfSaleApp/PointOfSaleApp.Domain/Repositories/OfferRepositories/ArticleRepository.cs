using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSaleApp.Domain.Repositories.OfferRepositories
{
    public class ArticleRepository : BaseRepository
    {
        private readonly OfferRepository _offerRepository;

        public ArticleRepository(PointOfSaleDbContext dbContext, OfferRepository offerRepository) : base(dbContext)
        {
            _offerRepository = offerRepository;
        }

        public (ResponseResultType Result, string Message) Add(string name, string description, int availableQuantity, decimal price)
        {
            (var parentResult, var message, var offer) = _offerRepository.Add(name, description, availableQuantity);

            if (!(offer is Offer))
                return (parentResult, message);

            if (price < 0.0m)
                return (ResponseResultType.ValidationError, "Price for article must be greater than 0.0");

            var article = new Article
            { 
                Price = price,
                Offer = DbContext.Offers.Find(offer.Id)
            };

            article.Offer.OfferType = OfferType.Article;

            DbContext.Articles.Add(article);

            var result = SaveChanges();
            return (result == ResponseResultType.Success) ? (ResponseResultType.Success, "Succesfully added article.") : (ResponseResultType.NoChanges, "No changes made.");
        }

        public ResponseResultType Edit(Article article, int articleId)
        {
            var edittingArticle = DbContext.Articles.Find(articleId);

            if(edittingArticle == null)
            {
                return ResponseResultType.NotFound;
            }

            _offerRepository.Edit(article.Offer, article.OfferId);

            edittingArticle.Price = article.Price;

            return SaveChanges();
        }

        public ICollection<Article> GetAll()
        {
            return DbContext.Articles
                .Include(a => a.Offer)
                .ToList();
        }
    }
}
