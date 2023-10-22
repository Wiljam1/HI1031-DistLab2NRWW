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
            Id = 1, // from seed data
            Title = "BMW E30",
            Description = "En schysst BMW. Typ i nyskick.",
            InitialPrice = 5000,
            CreatedDate = DateTime.Now,
            FinalDate = DateTime.Now.AddDays(1),
            UserName = "wiljam@kth.se",
            BidDbs = new List<BidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(adb);

        BidDb bdb1 = new BidDb()
        {
            Id = 1,
            UserName = "niklas@kth.se",
            Amount = 5500,
            PlacedBidTime = DateTime.Now,
            AuctionId = 1, 
        };
        BidDb bdb2 = new BidDb()
        {
            Id = 2,
            UserName = "inteWiljam@kth.se",
            Amount = 7500,
            PlacedBidTime = DateTime.Now,
            AuctionId = 1,
        };
        BidDb bdb3 = new BidDb()
        {
            Id = 3,
            UserName = "niklas@kth.se",
            Amount = 8000,
            PlacedBidTime = DateTime.Now,
            AuctionId = 1, 
        };
        BidDb bdb4 = new BidDb()
        {
            Id = 4,
            UserName = "inteWiljam@kth.se",
            Amount = 9000,
            PlacedBidTime = DateTime.Now,
            AuctionId = 1,
        };
        modelBuilder.Entity<BidDb>().HasData(bdb1);
        modelBuilder.Entity<BidDb>().HasData(bdb2);
        modelBuilder.Entity<BidDb>().HasData(bdb3);
        modelBuilder.Entity<BidDb>().HasData(bdb4);
    }

    public DbSet<AuctionApplication.Core.Auction> Auction { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.AuctionVM> AuctionVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.AuctionDetailsVM> AuctionDetailsVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.CreateAuctionVM> CreateAuctionVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.EditAuctionVM> EditAuctionVM { get; set; } = default!;

    public DbSet<AuctionApplication.ViewModels.CreateBidVM> CreateBidVM { get; set; } = default!;
}