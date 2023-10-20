namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAll();
        List<Auction> GetAllByUserName(string userName);
        Auction GetById(int id);
        void Add(Auction auction);
        void UpdateAuctionDescription(Auction auction);
    }
}