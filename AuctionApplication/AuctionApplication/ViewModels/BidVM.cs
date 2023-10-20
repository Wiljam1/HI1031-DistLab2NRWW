using AuctionApplication.Core;

namespace AuctionApplication.ViewModels;


// TODO: ViewModels kan fixas med automapper?
public class BidVM
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime PlacedBidTime { get; set; }

    public static BidVM FromBid(Bid bid)
    {
        return new BidVM
        {
            Id = bid.Id,
            UserName = bid.UserName,
            PlacedBidTime = bid.PlacedBidTime
        };
    }
}