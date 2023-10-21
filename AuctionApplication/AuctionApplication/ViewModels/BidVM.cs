using AuctionApplication.Core;

namespace AuctionApplication.ViewModels;

public class BidVM
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int Amount { get; set; }
    public DateTime PlacedBidTime { get; set; }

    public static BidVM FromBid(Bid bid)
    {
        return new BidVM
        {
            Id = bid.Id,
            UserName = bid.UserName,
            Amount = bid.Amount,
            PlacedBidTime = bid.PlacedBidTime
        };
    }
}