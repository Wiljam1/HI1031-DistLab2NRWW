using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AuctionApplication.Core;
using AuctionApplication.ViewModels;

namespace AuctionApplication.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }



    public DbSet<BidDb> BidDbs { get; set; }
    public DbSet<AuctionDb> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AuctionDb adb = new AuctionDb
        {
            Id = -1, // from seed data
            Title = "Learning ASP.NET Core with MVC",
            CreatedDate = DateTime.Now,
            UserName = "wiljam@kth.se",
            BidDbs = new List<BidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(adb);

        BidDb bdb1 = new BidDb()
        {
            Id = -1,
            Description = "Follow the turtorials",
            LastUpdated = DateTime.Now,
            Status = Core.Status.IN_PROGRESS,
            AuctionId = -1,
        };
        BidDb bdb2 = new BidDb()
        {
            Id = -2,
            Description = "Do it yourself!",
            LastUpdated = DateTime.Now,
            Status = Core.Status.NO_BID,
            AuctionId = -1
        };
        modelBuilder.Entity<BidDb>().HasData(bdb1);
        modelBuilder.Entity<BidDb>().HasData(bdb2);
    }

    public DbSet<AuctionApplication.Core.Auction> Auction { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.AuctionVM> AuctionVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.AuctionDetailsVM> AuctionDetailsVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.CreateAuctionVM> CreateAuctionVM { get; set; } = default!;
}