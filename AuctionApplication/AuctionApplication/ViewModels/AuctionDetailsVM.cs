using AuctionApplication.Core;

namespace AuctionApplication.ViewModels;

public class AuctionDetailsVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsCompleted { get; set; }
    public List<BidVM> BidVMs { get; set; } = new();

    public static AuctionDetailsVM FromAuction(Auction auction)
    {
        var detailsVM = new AuctionDetailsVM()
        {
            Id = auction.Id,
            Title = auction.Title,
            CreatedDate = auction.CreatedDate,
            IsCompleted = auction.IsCompleted()
        };
        foreach (var bid in auction.Bids)
        {
            detailsVM.BidVMs.Add(BidVM.FromBid(bid));
        }
        return detailsVM;
    }
}