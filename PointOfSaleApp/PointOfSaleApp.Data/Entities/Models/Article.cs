using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Article
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public Article()
        {
        }

        public Article(Offer offer)
        {
            ArticleOfferId = offer.Id;
            ArticleOffer = offer;
        }
    }
}
