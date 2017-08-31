using System;
using System.ComponentModel.DataAnnotations;

namespace Ares.Models
{
    public class Offer
    {
        public int ID { get; set; }
        public int AuctionItemID { get; set; }

        [Display(Name = "Buyer Name")]
        public string BuyerName { get; set; }

        [Display(Name = "Offer Time")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime OfferTime { get; set; }

        [Display(Name = "Offer Amount")]
        public decimal OfferAmount { get; set; }

        [Display(Name = "Auction Item")]
        public AuctionItem AuctionItem { get; set; }

    }
}
