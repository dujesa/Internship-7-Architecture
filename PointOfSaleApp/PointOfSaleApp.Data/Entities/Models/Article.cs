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
        public Offer ArticleOffer { get; set; }

        public Article()
        {
        }

        public Article(Offer articleOffer)
        {
            ArticleOffer = articleOffer;
        }
    }
}
