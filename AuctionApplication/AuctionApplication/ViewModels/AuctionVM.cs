using AuctionApplication.Core;

namespace AuctionApplication.ViewModels;

public class AuctionVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsCompleted { get; set; }

    public static AuctionVM FromAuction(Auction auction)
    {
        return new AuctionVM()
        {
            Id = auction.Id,
            Title = auction.Title,
            CreatedDate = auction.CreatedDate,
            IsCompleted = auction.IsCompleted()
        };
    }
}