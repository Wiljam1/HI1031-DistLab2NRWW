namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();
        List<Auction> GetAllActive();
        List<Auction> GetWonAuctions(string username);
        List<Auction> GetActiveAuctionsWithBid(string username);
        Auction GetById(int id);
        void Add(Auction auction);
        void Add(Bid bid, int auctionId);
        void UpdateAuctionDescription(Auction auction);
    }
}