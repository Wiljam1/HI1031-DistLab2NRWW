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

    // TODO: Reworka den här likt getById
    public List<Auction> GetAll()
    {
        //var auctionDbs = _dbContext.AuctionDbs.ToList();
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
        //eager loading
        //var auctionDb = _dbContext.AuctionDbs
        //    .Include(a => a.BidDbs)
        //    .Where(a => a.Id == id)
        //    .SingleOrDefault();

        // kanske inte eager loading längre
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

    public void UpdateAuctionDescription(Auction auction)
    {
        AuctionDb auctiondb = _mapper.Map<AuctionDb>(auction);
        _unitOfWork.AuctionRepository.Update(auctiondb);
        _unitOfWork.Save();
    }
}