namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();
        Auction GetById(int id);
        void Add(Auction auction);
        void Add(Bid bid, int auctionId);
        void UpdateAuctionDescription(Auction auction);
    }
}