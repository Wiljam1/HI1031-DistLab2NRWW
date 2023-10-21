namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAllActive();
        List<Auction> GetWonAuctions(string userName);
        List<Auction> GetActiveAuctionsWithBid(string username);
        Auction GetById(int id);
        void Add(Auction auction);
        void Add(Bid bid, int auctionId);
        void UpdateAuctionDescription(Auction auction);
        int GetHighestBidForAuction(Auction auction);
    }
}