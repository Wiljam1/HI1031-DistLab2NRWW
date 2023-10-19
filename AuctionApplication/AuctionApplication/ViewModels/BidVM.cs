using AuctionApplication.Core;

namespace AuctionApplication.ViewModels;


// TODO: ViewModels kan fixas med automapper?
public class BidVM
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime LastUpdated { get; set; }
    public Status Status { get; set; }

    public static BidVM FromBid(Bid bid)
    {
        return new BidVM
        {
            Id = bid.Id,
            Description = bid.Description,
            LastUpdated = bid.LastUpdated,
            Status = bid.Status
        };
    }
}