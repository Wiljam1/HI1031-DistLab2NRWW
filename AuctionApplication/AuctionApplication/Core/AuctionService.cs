using AuctionApplication.Core.Interfaces;


namespace AuctionApplication.Core;

public class AuctionService : IAuctionService
{
    private IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }

    public bool Add(Auction auction)
    {
        if (auction.Id != 0 || auction.IsExpired())
            return false;

        _auctionPersistence.Add(auction);
        return true;
    }

    public bool Add(Bid bid, int auctionId)
    {
        if (bid.Id != 0 || bid.Amount < 0) 
            return false;
        var auction = GetById(auctionId);
        if (GetHighestBidForAuction(auction) >= bid.Amount || auction.IsExpired())
            return false;

        _auctionPersistence.Add(bid, auctionId);
        return true;
    }

    public List<Auction> GetAllActive()
    { 
        return _auctionPersistence.GetAllActive();
    }

    public Auction GetById(int id)
    {
        var auction = _auctionPersistence.GetById(id);
        IEnumerable<Bid> sortedBids = auction.GetSortedBidsByAmount();

        var auctionWithSortedBids = new Auction
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            UserName = auction.UserName,
            InitialPrice = auction.InitialPrice,
            CreatedDate = auction.CreatedDate,
            FinalDate = auction.FinalDate,
        };
        foreach (var bid in sortedBids)
        {
            auctionWithSortedBids.AddBid(bid);
        }

        return auctionWithSortedBids;
    }

    public int GetHighestBidForAuction(Auction auction)
    {
        var auctionBids = auction.Bids;
        int highestBid = 0;
        foreach (var bid in auctionBids)
        {
            if(bid.Amount > highestBid)
                highestBid = bid.Amount;
        }
        return Math.Max(highestBid, auction.InitialPrice);
    }

    public List<Auction> GetActiveAuctionsWithBid(string username)
    {
        return _auctionPersistence.GetActiveAuctionsWithBid(username);
    }

    public List<Auction> GetWonAuctions(string username)
    {
        return _auctionPersistence.GetWonAuctions(username);
    }

    public void UpdateAuctionDescription(Auction auction)
    {
       _auctionPersistence.UpdateAuctionDescription(auction);
    }
}