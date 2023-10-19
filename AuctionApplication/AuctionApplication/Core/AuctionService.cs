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

    public List<Auction> GetAll()
    {
        return _auctionPersistence.GetAll();
    }

    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctionPersistence.GetAllByUserName(userName);
    }

    public Auction GetById(int id)
    {
        return _auctionPersistence.GetById(id);
    }
}