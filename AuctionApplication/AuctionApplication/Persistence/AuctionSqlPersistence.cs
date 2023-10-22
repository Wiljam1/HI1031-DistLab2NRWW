using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AutoMapper;

namespace AuctionApplication.Persistence;

public class AuctionSqlPersistence : IAuctionPersistence
{
    private UnitOfWork _unitOfWork;
    private IMapper _mapper;

    public AuctionSqlPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _unitOfWork = new UnitOfWork(dbContext);
        _mapper = mapper;
    }

    public void Add(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _unitOfWork.AuctionRepository.Insert(adb);
        _unitOfWork.Save();
    }

    public void Add(Bid bid, int auctionId)
    {
        BidDb bdb = _mapper.Map<BidDb>(bid);
        bdb.AuctionId = auctionId; //FK

        _unitOfWork.BidRepository.Insert(bdb);
        _unitOfWork.Save();
    }
    public void UpdateAuctionDescription(Auction auction)
    {
        AuctionDb auctiondb = _mapper.Map<AuctionDb>(auction);
        _unitOfWork.AuctionRepository.Update(auctiondb);
        _unitOfWork.Save();
    }

    public List<Auction> GetAll()
    {
        var auctionDbs = _unitOfWork.AuctionRepository.Get(includeProperties: "BidDbs")
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            foreach (var bid in adb.BidDbs)
            {
                auction.AddBid(_mapper.Map<Bid>(bid));
            }
            result.Add(auction);
        }
        return result;
    }

    public Auction GetById(int id)
    {
        var auctionDb = _unitOfWork.AuctionRepository.Get(includeProperties: "BidDbs")
            .Where(a => a.Id == id)
            .SingleOrDefault();
       
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bdb in auctionDb.BidDbs)
        {
            auction.AddBid(_mapper.Map<Bid>(bdb));
        }

        return auction;
    }

    public List<Auction> GetAllActive()
    {
        var allAuctions = GetAll();

        // Filter out auctions that have passed FinalDate & order by FinalDate
        var activeAuctions = allAuctions
            .Where(a => a.FinalDate > DateTime.Now)
            .OrderBy(a => a.FinalDate)
            .ToList();

        return activeAuctions;
    }

    public List<Auction> GetWonAuctions(string username)
    {
        var auctions = GetAll();

        var wonAuctions = auctions
            .Where(auction =>
            {
                int highestBid = GetHighestBidForAuction(auction);
                if (highestBid != auction.InitialPrice)
                {
                    var winningBid = auction.Bids.FirstOrDefault(bid => bid.Amount == highestBid && bid.UserName.Equals(username));
                    return winningBid != null && auction.IsExpired();
                }
                return auction.UserName.Equals(username) && auction.FinalDate < DateTime.Now;
            })
            .ToList();

        return wonAuctions;
    }

    public List<Auction> GetActiveAuctionsWithBid(string username)
    {
        var allActiveAuctions = GetAllActive();

        var activeAuctionsWithBidFromUser = allActiveAuctions
            .Where(auction => auction.Bids.Any(bid => bid.UserName.Equals(username)))
            .ToList();

        return activeAuctionsWithBidFromUser;
    }

    private int GetHighestBidForAuction(Auction auction)
    {
        var auctionBids = auction.Bids;
        int highestBid = 0;
        foreach (var bid in auctionBids)
        {
            if (bid.Amount > highestBid)
                highestBid = bid.Amount;
        }
        return Math.Max(highestBid, auction.InitialPrice);
    }
}