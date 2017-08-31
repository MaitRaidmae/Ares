using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ares.Models
{
    public class AuctionItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        [Display(Name = "Auction End Time")]
        public DateTime AuctionEndTime { get; set; }
        [IgnoreDataMember]
        public AuctionItemState? ItemState { get; set; }
        [IgnoreDataMember]
        public List<Offer> Offers { get; set; }
    }

    public enum AuctionItemState
    {
        Active,
        Inactive,
        Ended
    }
}
