using Ares.Models;
using System;
using System.Linq;

namespace Ares.Data
{
    public class DBInitializer
    {
        public static void Initialize(AresContext context)
        {
            context.Database.EnsureCreated();

            if (context.AuctionItem.Any())
            {
                return;   // DB has been seeded
            }

            var auctionItems = new AuctionItem[]
            {
                new AuctionItem{Name="Big Bad Wolf",Description="A wooden statue of a wolf, that is bad", AuctionEndTime=DateTime.Parse("2017-12-31"),ItemState = AuctionItemState.Active},
                new AuctionItem{Name="Small Fluffy Bunny",Description="A cute fluffy bunny with deep red eyes and slighly bloody fangs", AuctionEndTime=DateTime.Parse("2018-06-15"),ItemState = AuctionItemState.Active},
                new AuctionItem{Name="Wet Fish",Description="A fish in it's natural state", AuctionEndTime=DateTime.Parse("2017-11-12"),ItemState = AuctionItemState.Inactive},
                new AuctionItem{Name="Big Good Wolf",Description="Looks like a Big Bad Wolf, but is a Good Boye", AuctionEndTime=DateTime.Parse("2017-08-31"),ItemState = AuctionItemState.Inactive}
            };

            foreach (AuctionItem a in auctionItems)
            {
                context.AuctionItem.Add(a);
            }
            context.SaveChanges();


            var offers = new Offer[]
            {
                new Offer{AuctionItemID=1,BuyerName="Johnny Bravo",OfferTime=DateTime.Parse("2016-09-01"),OfferAmount = 230.43M},
                new Offer{AuctionItemID=2,BuyerName="Will Smith",OfferTime=DateTime.Parse("2017-03-06"),OfferAmount = 212.43M},
                new Offer{AuctionItemID=3,BuyerName="Angela DeVille",OfferTime=DateTime.Parse("2017-04-01"),OfferAmount = 5000M},
                new Offer{AuctionItemID=1,BuyerName="Viljar Sepp",OfferTime=DateTime.Parse("2015-09-22"),OfferAmount = 12.54M},
                new Offer{AuctionItemID=3,BuyerName="Mister Smith",OfferTime=DateTime.Parse("2017-02-12"),OfferAmount = 123424.33M},
                new Offer{AuctionItemID=2,BuyerName="Thomas Albert Anderson",OfferTime=DateTime.Parse("2017-06-23"),OfferAmount = 42M},
            };
            foreach (Offer o in offers)
            {
                context.Offer.Add(o);
            }
            context.SaveChanges();

        }
    }
}
