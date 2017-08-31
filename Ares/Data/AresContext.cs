using Microsoft.EntityFrameworkCore;

namespace Ares.Data
{
    public class AresContext : DbContext
    {
        public AresContext (DbContextOptions<AresContext> options)
            : base(options)
        {
        }

        public DbSet<Ares.Models.AuctionItem> AuctionItem { get; set; }
        public DbSet<Ares.Models.Offer> Offer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Offer>()
                .HasOne(ai => ai.AuctionItem)
                .WithMany(b => b.Offers)
                .HasForeignKey(p => p.AuctionItemID)
                .HasConstraintName("ForeignKey_Offer_AuctionItem");
        }

    }
}
