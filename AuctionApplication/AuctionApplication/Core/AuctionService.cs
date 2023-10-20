using AuctionApplication.Core.Interfaces;


namespace AuctionApplication.Core;

public class AuctionService : IAuctionService
{
    private IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }

    public void Add(Auction auction)
    {
        //assume no bids on new auction 
        //TODO: Gör Factory-mönster i Core där man skickar in värden och får korrekta objekt hit.
        if (auction == null || auction.Id != 0) throw new InvalidDataException();
        auction.CreatedDate = DateTime.Now;
        _auctionPersistence.Add(auction);
    }

    public void Add(Bid bid, int auctionId)
    {
        if (bid == null || bid.Id != 0) throw new InvalidDataException();
        _auctionPersistence.Add(bid, auctionId);
    }

    public List<Auction> GetAll()
    { 
        var allAuctions = _auctionPersistence.GetAll();

        // Filter out auctions that have passed FinalDate & order by FinalDate
        var activeAuctions = allAuctions
            .Where(a => a.FinalDate > DateTime.Now)
            .OrderBy(a => a.FinalDate)
            .ToList();

        return activeAuctions;
    }

    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctionPersistence.GetAllByUserName(userName);
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

    public List<Auction> getMyWonAuctions(string userName)
    {
        List<Auction> auctions = GetAll();
        List<Auction> myWonAuctions = new List<Auction>();
        foreach (var auction in auctions)
        {
            int highestBid = GetHighestBidForAuction(auction);
            if (highestBid != auction.InitialPrice)
            {
                foreach (var bid in auction.Bids)
                {
                    if(highestBid == bid.Amount && bid.UserName.Equals(userName) && auction.FinalDate < DateTime.Now )
                    {
                        myWonAuctions.Add(auction); break;
                    }
                }
            }
            else if (auction.UserName.Equals(userName))     //adds a won auction if no one bidded on the auction to the one whos auction it was
            {
                myWonAuctions.Add(auction);
            }
        }
        return myWonAuctions;
    }

    public void UpdateAuctionDescription(Auction auction)
    {
       _auctionPersistence.UpdateAuctionDescription(auction);
    }
}