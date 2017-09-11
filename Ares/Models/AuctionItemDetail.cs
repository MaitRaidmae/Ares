using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ares.Models
{
    public class AuctionItemDetail
    {
        public AuctionItem Item { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}
